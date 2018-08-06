using System;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Helper = Neo.SmartContract.Framework.Helper;
using System.Text;

using System.ComponentModel;
using System.Numerics;
using System.Collections.Generic;


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
		public static BigInteger blocksPerDay = 4320;
		//某年的结束区块高度
		public static BigInteger BlockheightOfYear(BigInteger year){
			return blocksPerDay * year + Storage.Get(Storage.CurrentContext, keyGenesisHeight).AsBigInteger();
		}
        
        
        //丕料类型定义
		public static class Pimetal
        {
			public static int Water = 0;
			public static int Soil = 1;
			public static int Wind = 2;
			public static int Fire = 3;
        }

        //海盗船特性类型定义
		public static class Feature
        {
			public static int Attack = 0;
			public static int Defence = 1;
			public static int Speed = 2;
			public static int HP = 3;
			public static int Recover = 4;
        }
        
		//
		//owner帐号负责在每“年”开始前基于NASDAQ收盘价设定该年四种丕料的数量。如果没能在该年开始前成功调用此函数，则默认四丕料比例相同。
		//将某量丕料散步到不同位置去(每两个字节为一个坐标值)
        //四种丕料分四次call，节省GAS费用
		public const string keyPimetal = "pim";
		public static bool AllocatePimetal(BigInteger pimetalId, BigInteger amount){
			if (Runtime.CheckWitness(Owner))
            {
                BigInteger yearNext = GetYear() + 1;

				byte[] data = new byte[0];
                for (BigInteger i = 0; i < amount * 2; i++)
                {    //随机X和Y轴，所以amountx2
					data = data.Concat(RandomTwoBytes());
                }

				Storage.Put(Storage.CurrentContext, keyPimetal + pimetalId, data);

                return true;
            }
            else
            {
                return false;
            }         
		}


        
		public static byte[] RandomTwoBytes(){
			//TBD
			return new byte[2] { 12, 31 };
		}
      
		public const string keyPlayer = "player";
		[Serializable]
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
        

		public static Player FindPlayer(byte[] addr){
			return (Player)(Storage.Get(Storage.CurrentContext, keyPlayer + addr)).Deserialize();
		}

        //玩家在前端真正保持持有丕料3分钟后，客户端（或服务器）才会调用此函数收集该丕料
		public static bool Collect(byte[] invoker, byte[] location){
			byte[] invalidLoc = { 0, 0, 0, 0 };
			if (location == invalidLoc) return false;


			BigInteger typePimetal = -1;
        
			for (BigInteger i = 0; i < 3;i++){
				byte[] locs = Storage.Get(Storage.CurrentContext, keyPimetal + i);
				for (int j = 0; j < locs.Length; j+=4){
					if(locs[j] == location[0] && locs[j+1] == location[1] &&
					   locs[j+2] == location[2] && locs[j + 3] == location[3]){
						typePimetal = i;
						//更新该处内存为00
						byte[] newData = new byte[0];
						for (int k = 0; k < locs.Length; k++ ){
							if(k<j || k>= j+3){
								byte[] newVal = new byte[1] { locs[k] };
								newData = newData.Concat(newVal);
							}
							else if (k<j+3){
								byte[] newVal = new byte[1] { 0 };
								newData = newData.Concat(newVal);
							}

						}
						Storage.Put(Storage.CurrentContext, keyPimetal + typePimetal, newData);
						                  
						break;
					}
				}
			}

			Player player = FindPlayer(invoker);
			if (typePimetal == -1) return false;
			if( typePimetal == Pimetal.Water){
				player.water++;
			}
			else if (typePimetal == Pimetal.Soil)
            {
				player.soil++;
            }
			else if (typePimetal == Pimetal.Wind)
            {
				player.wind++;
            }
			else if (typePimetal == Pimetal.Fire)
            {
				player.fire++;
            }

			byte[] newPlayerData = player.Serialize();
			Storage.Put(Storage.CurrentContext, keyPimetal + typePimetal, newPlayerData);
			return true;
		}
        
		public static bool Upgrade(byte[] invoker, BigInteger feature){
			Player player = FindPlayer(invoker);
			BigInteger lvl = -1;
			if (feature == 0) lvl = player.lvlAtk;
			else if (feature == 1) lvl = player.lvlDef;
			else if (feature == 2) lvl = player.lvlSpd;
			else if (feature == 3) lvl = player.lvlHP;
			else if (feature == 4) lvl = player.lvlRev;

			BigInteger[] pimetals = AmountUpgrade(feature, lvl + 1);
			if( player.water >= pimetals[0] && player.soil >= pimetals[1] &&
			   player.wind >= pimetals[2] && player.fire >= pimetals[3]){
				player.water -= pimetals[0];
				player.soil -= pimetals[1];
				player.wind -= pimetals[2];
				player.fire -= pimetals[3];

				if (feature == 0) player.lvlAtk ++;
                else if (feature == 1) player.lvlDef++;
                else if (feature == 2) player.lvlSpd ++;
                else if (feature == 3) player.lvlHP ++;
                else if (feature == 4) player.lvlRev ++;
				return true;
			}
			else{
				return false;
			}
		}
        
       
        //发生灾难，
		public static void Catastrophe(){
			byte[] random = RandomTwoBytes();
			int type = random[0] % 4;
            //TBD
		}

        //升级所需各种元素的量
		public static BigInteger[] AmountUpgrade(BigInteger feature, BigInteger level){
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
		public static bool Genesis(){
			if (Runtime.CheckWitness(Owner)){
				//年份实际从1开始。第一区块无丕料
				Storage.Put(Storage.CurrentContext, keyYear, 0);  //年份为0

				BigInteger water = 100;
				BigInteger soil = 100;
				BigInteger wind = 100;
				BigInteger fire = 100;

				AllocatePimetal(Pimetal.Water,water);
				AllocatePimetal(Pimetal.Soil, soil);
				AllocatePimetal(Pimetal.Wind, wind);
				AllocatePimetal(Pimetal.Fire, fire);
				return true;
			}
			else{
				return false;
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
				//BigInteger numCards = (BigInteger)args[0] * (BigInteger)args[1];
				return Collect((byte[])args[0], (byte[])args[1]);
            }
            return false;
        }
    }

}
