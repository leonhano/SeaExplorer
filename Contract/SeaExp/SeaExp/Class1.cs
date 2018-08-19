using System;
using System.Text;

using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;

using Neunity.Tools;

#if NEOSC
using Neunity.Adapters.NEO;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using Helper = Neo.SmartContract.Framework.Helper;
using System.Threading;
#else
using Neunity.Adapters.Unity;
#endif



namespace SeaExp
{
	public class Class1: SmartContract
    {
		//当前年度
		public const string keyYear = "kYear";      
		public static BigInteger GetYear(){
			return Storage.Get(Storage.CurrentContext, keyYear).AsBigInteger();
		}



		public const string keyGenesisHeight = "kGHeight";
        public const int blocksPerDay = 4320;
        //public const ulong PiemetalCallAmount = 10;

		//某年的结束区块高度
		public static BigInteger BlockheightOfYear(BigInteger year){
            BigInteger bpd = blocksPerDay;
            return bpd * year + Storage.Get(Storage.CurrentContext, keyGenesisHeight).AsBigInteger();
		}
        
        
        //丕料类型定义
		public static class Pimetal
        {
            public static readonly int Water = 0;
            public static readonly int Soil = 1;
            public static readonly int Wind = 2;
            public static readonly int Fire = 3;
        }

        //海盗船特性类型定义
		public static class Feature
        {
            public static readonly int Attack = 0;
            public static readonly int Defence = 1;
            public static readonly int Speed = 2;
            public static readonly int HP = 3;
            public static readonly int Recover = 4;
        }
        

		public const string keyPimetal = "pim";

        //唯有特权帐户允许从NASDAQ收盘价格读取数据后更新新增Pimetal的布置
        //在同一周期里对每种丕料pimetalId可以有invokeTime次调用或修改(如果错误的话)
        public static byte[] AllocatePimetal(BigInteger pimetalId,BigInteger invokeTime){
			if (Runtime.CheckWitness(Owner))
            {
                BigInteger yearNext = GetYear() + 1;

                Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
                byte[] thisData = tx.Hash;  //使用TxId生成随机数

                int startIndex = (int)invokeTime * thisData.Length;
                byte[] totalData = NuIO.GetStorageWithKeyPath(keyPimetal, Op.BigInt2String(pimetalId));
                byte[] startData = Op.SubBytes(totalData, 0, startIndex);
                byte[] endData = Op.SubBytes(totalData, startIndex + thisData.Length, totalData.Length - startIndex - thisData.Length);
                byte[] newData = Op.JoinByteArray(startData, thisData, endData);

                NuIO.SetStorageWithKeyPath(newData, keyPimetal, Op.BigInt2String(pimetalId) );

                return NuTP.RespDataSuccess();
            }
            else
            {
                return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.Unauthorized);
            }         
		}


        
		public static byte[] RandomTwoBytes(){
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            return tx.Hash.Range(0,2);

		}
      
		public const string keyPlayer = "player";
		

		public class Player{
			public byte[] address;
			public BigInteger lvlAtk;
			public BigInteger lvlDef;
			public BigInteger lvlSpd;
			public BigInteger lvlHP;
			public BigInteger lvlRev;
			public BigInteger lastUpdateYear;
			public BigInteger water;
			public BigInteger soil;
			public BigInteger wind;
			public BigInteger fire;
		}

        public static Player Bytes2Player(byte[] ba){
            return new Player
            {
                address = ba.SplitTbl(0),
                lvlAtk = ba.SplitTblInt(1),
                lvlDef = ba.SplitTblInt(2),
                lvlSpd = ba.SplitTblInt(3),
                lvlHP = ba.SplitTblInt(4),
                lvlRev = ba.SplitTblInt(5),
                lastUpdateYear = ba.SplitTblInt(6),
                water = ba.SplitTblInt(7),
                soil = ba.SplitTblInt(8),
                wind = ba.SplitTblInt(9),
                fire = ba.SplitTblInt(10)
            };
        }

        public static byte[] Player2Bytes(Player player){
            return NuSD.Seg(player.address)
                       .AddSegInt(player.lvlAtk)
                       .AddSegInt(player.lvlDef)
                       .AddSegInt(player.lvlSpd)
                       .AddSegInt(player.lvlHP)
                       .AddSegInt(player.lvlRev)
                       .AddSegInt(player.lastUpdateYear)
                       .AddSegInt(player.water)
                       .AddSegInt(player.soil)
                       .AddSegInt(player.wind)
                       .AddSegInt(player.fire);
        }

		public static Player FindPlayer(byte[] addr){
            byte[] data = NuIO.GetStorageWithKeyPath(keyPlayer,Op.Bytes2String(addr));
            return Bytes2Player(data);
		}


        public static byte[] Collect(byte[] invoker, BigInteger type, byte[] location){
            byte[] invalidLoc = new byte[4]{ 0, 0, 0, 0 };
            if (location == invalidLoc) return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.BadRequest);


			int typePimetal = 99;
        
			for (int i = 0; i < 3;i++){
                byte[] locs = NuIO.GetStorageWithKeyPath(keyPimetal,Op.BigInt2String(type));
				//byte[] locs = Storage.Get(Storage.CurrentContext, keyPimetal + i);
				for (int j = 0; j < locs.Length; j+=4){
					if(locs[j] == location[0] && locs[j+1] == location[1] &&
					   locs[j+2] == location[2] && locs[j + 3] == location[3]){
						typePimetal = i;
						//更新该处内存为00
						byte[] newData = new byte[0];
						for (int k = 0; k < locs.Length; k++ ){
							if(k<j || k>= j+3){
								byte[] newVal = new byte[1] { locs[k] };
                                newData = Op.JoinTwoByteArray(newData, newVal);
								//newData = newData.Concat(newVal);
							}
							else if (k<j+3){
								byte[] newVal = new byte[1] { 0 };
                                newData = Op.JoinTwoByteArray(newData, newVal);
							}

						}
                        NuIO.SetStorageWithKeyPath(newData,keyPimetal, Op.BigInt2String(typePimetal));						                  
						break;
					}
				}
			}

			Player player = FindPlayer(invoker);
            if (typePimetal == 99) return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.BadRequest);    //非法输入值
			if( typePimetal == Pimetal.Water){
				player.water+=1;
			}
			else if (typePimetal == Pimetal.Soil)
            {
                player.soil+= 1;
            }
			else if (typePimetal == Pimetal.Wind)
            {
                player.wind += 1;
            }
			else if (typePimetal == Pimetal.Fire)
            {
                player.fire+= 1;
            }

            byte[] newPlayerData = Player2Bytes(player);
            NuIO.SetStorageWithKeyPath(newPlayerData, keyPimetal, Op.BigInt2String(typePimetal));
            return NuTP.RespDataSuccess();
		}
        
        public static byte[] Upgrade(byte[] invoker, BigInteger feature){
			Player player = FindPlayer(invoker);
			BigInteger lvl = -1;
			if (feature == 0) lvl = player.lvlAtk;
			else if (feature == 1) lvl = player.lvlDef;
			else if (feature == 2) lvl = player.lvlSpd;
			else if (feature == 3) lvl = player.lvlHP;
			else if (feature == 4) lvl = player.lvlRev;
            int f = Op.BigInt2Int(feature);
			BigInteger[] pimetals = AmountUpgrade(f, lvl + 1);
			if( player.water >= pimetals[0] && player.soil >= pimetals[1] &&
			   player.wind >= pimetals[2] && player.fire >= pimetals[3]){
				player.water -= pimetals[0];
				player.soil -= pimetals[1];
				player.wind -= pimetals[2];
				player.fire -= pimetals[3];

				if (feature == 0) player.lvlAtk += 1;
                else if (feature == 1) player.lvlDef+= 1;
                else if (feature == 2) player.lvlSpd += 1;
                else if (feature == 3) player.lvlHP += 1;
                else if (feature == 4) player.lvlRev += 1;
                return NuTP.RespDataSuccess();
			}
			else{
                return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.BadRequest);
			}
		}
        
       
        //发生灾难，
		public static void Catastrophe(){
			byte[] random = RandomTwoBytes();
			int type = random[0] % 4;
            //TBD
		}

        //升级所需各种元素的量
		public static BigInteger[] AmountUpgrade(int feature, BigInteger level){
			BigInteger[] amounts = new BigInteger[4];
			if(feature == Feature.Attack){
				amounts[Pimetal.Water] = level * 0;
				amounts[Pimetal.Soil] = level * 1;
				amounts[Pimetal.Wind] = level * 2;
				amounts[Pimetal.Fire] = level * 3;
			}
			if (feature == Feature.Defence)
            {
                amounts[Pimetal.Water] = level * 2;
                amounts[Pimetal.Soil] = level * 3;
                amounts[Pimetal.Wind] = level * 1;
                amounts[Pimetal.Fire] = level * 0;
            }
			if (feature == Feature.Speed)
            {
                amounts[Pimetal.Water] = level * 1;
                amounts[Pimetal.Soil] = level * 0;
                amounts[Pimetal.Wind] = level * 3;
                amounts[Pimetal.Fire] = level * 2;
            }
			if (feature == Feature.HP)
            {
                amounts[Pimetal.Water] = level * 1;
                amounts[Pimetal.Soil] = level * 1;
                amounts[Pimetal.Wind] = level * 1;
                amounts[Pimetal.Fire] = level * 1;
            }
			if (feature == Feature.Recover)
            {
                amounts[Pimetal.Water] = level * 3;
                amounts[Pimetal.Soil] = level * 2;
                amounts[Pimetal.Wind] = level * 0;
                amounts[Pimetal.Fire] = level * 1;
            }
			return amounts;
		}



        //创世初始化
        public static byte[] Genesis(){
			if (Runtime.CheckWitness(Owner)){
                //年份实际从1开始。第一区块无丕料
                byte[] startYear = new byte[1]{ 1 };
                NuIO.SetStorageWithKey(keyYear,startYear );
				//Storage.Put(Storage.CurrentContext, keyYear, 0);  //年份为0

				BigInteger water = 100;
				BigInteger soil = 100;
				BigInteger wind = 100;
				BigInteger fire = 100;

				AllocatePimetal(Pimetal.Water,water);
				AllocatePimetal(Pimetal.Soil, soil);
				AllocatePimetal(Pimetal.Wind, wind);
				AllocatePimetal(Pimetal.Fire, fire);
                return NuTP.RespDataSuccess();
			}
			else{
                return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.Unauthorized);
			}
		}




		public static readonly byte[] Owner = "AK2nJJpJr6o664CWJKi1QRXjqeic2zRp8y".ToScriptHash();


		public static Object Main(string operation, params object[] args)
        {
            if (operation == "genesis")
            {
				return Genesis();

            }

            if (operation == "upgrade")
            {
				return Upgrade((byte[])args[0],(BigInteger)args[1]);
            }

            if (operation == "collect")
            {
                return Collect((byte[])args[0], (BigInteger)args[1],(byte[])args[2]);
            }
            return NuTP.RespDataWithCode(NuTP.SysDom,NuTP.Code.NotFound);
        }


    }

}
