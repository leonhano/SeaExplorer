template <typename T> void RegisterClass();
template <typename T> void RegisterStrippedType(int, const char*, const char*);

void InvokeRegisterStaticallyLinkedModuleClasses()
{
	// Do nothing (we're in stripping mode)
}

void RegisterStaticallyLinkedModulesGranular()
{
	void RegisterModule_Animation();
	RegisterModule_Animation();

	void RegisterModule_Audio();
	RegisterModule_Audio();

	void RegisterModule_CloudWebServices();
	RegisterModule_CloudWebServices();

	void RegisterModule_Physics();
	RegisterModule_Physics();

	void RegisterModule_Terrain();
	RegisterModule_Terrain();

	void RegisterModule_TerrainPhysics();
	RegisterModule_TerrainPhysics();

	void RegisterModule_TextRendering();
	RegisterModule_TextRendering();

	void RegisterModule_UI();
	RegisterModule_UI();

	void RegisterModule_UnityConnect();
	RegisterModule_UnityConnect();

	void RegisterModule_IMGUI();
	RegisterModule_IMGUI();

	void RegisterModule_Core();
	RegisterModule_Core();

	void RegisterModule_SharedInternals();
	RegisterModule_SharedInternals();

	void RegisterModule_JSONSerialize();
	RegisterModule_JSONSerialize();

	void RegisterModule_UnityWebRequest();
	RegisterModule_UnityWebRequest();

	void RegisterModule_Wind();
	RegisterModule_Wind();

}

class EditorExtension; template <> void RegisterClass<EditorExtension>();
namespace Unity { class Component; } template <> void RegisterClass<Unity::Component>();
class Behaviour; template <> void RegisterClass<Behaviour>();
class Animation; template <> void RegisterClass<Animation>();
class Animator; template <> void RegisterClass<Animator>();
class AudioBehaviour; template <> void RegisterClass<AudioBehaviour>();
class AudioListener; template <> void RegisterClass<AudioListener>();
class AudioSource; template <> void RegisterClass<AudioSource>();
class AudioFilter; 
class AudioChorusFilter; 
class AudioDistortionFilter; 
class AudioEchoFilter; 
class AudioHighPassFilter; 
class AudioLowPassFilter; 
class AudioReverbFilter; 
class AudioReverbZone; 
class Camera; template <> void RegisterClass<Camera>();
namespace UI { class Canvas; } template <> void RegisterClass<UI::Canvas>();
namespace UI { class CanvasGroup; } template <> void RegisterClass<UI::CanvasGroup>();
namespace Unity { class Cloth; } 
class Collider2D; 
class BoxCollider2D; 
class CapsuleCollider2D; 
class CircleCollider2D; 
class CompositeCollider2D; 
class EdgeCollider2D; 
class PolygonCollider2D; 
class TilemapCollider2D; 
class ConstantForce; 
class Effector2D; 
class AreaEffector2D; 
class BuoyancyEffector2D; 
class PlatformEffector2D; 
class PointEffector2D; 
class SurfaceEffector2D; 
class FlareLayer; template <> void RegisterClass<FlareLayer>();
class GUIElement; template <> void RegisterClass<GUIElement>();
namespace TextRenderingPrivate { class GUIText; } 
class GUITexture; template <> void RegisterClass<GUITexture>();
class GUILayer; template <> void RegisterClass<GUILayer>();
class GridLayout; 
class Grid; 
class Tilemap; 
class Halo; 
class HaloLayer; 
class Joint2D; 
class AnchoredJoint2D; 
class DistanceJoint2D; 
class FixedJoint2D; 
class FrictionJoint2D; 
class HingeJoint2D; 
class SliderJoint2D; 
class SpringJoint2D; 
class WheelJoint2D; 
class RelativeJoint2D; 
class TargetJoint2D; 
class LensFlare; 
class Light; template <> void RegisterClass<Light>();
class LightProbeGroup; 
class LightProbeProxyVolume; 
class MonoBehaviour; template <> void RegisterClass<MonoBehaviour>();
class NavMeshAgent; 
class NavMeshObstacle; 
class OffMeshLink; 
class PhysicsUpdateBehaviour2D; 
class ConstantForce2D; 
class PlayableDirector; 
class Projector; 
class ReflectionProbe; 
class Skybox; template <> void RegisterClass<Skybox>();
class SortingGroup; 
class Terrain; template <> void RegisterClass<Terrain>();
class VideoPlayer; 
class WindZone; 
namespace UI { class CanvasRenderer; } template <> void RegisterClass<UI::CanvasRenderer>();
class Collider; template <> void RegisterClass<Collider>();
class BoxCollider; template <> void RegisterClass<BoxCollider>();
class CapsuleCollider; 
class CharacterController; template <> void RegisterClass<CharacterController>();
class MeshCollider; 
class SphereCollider; template <> void RegisterClass<SphereCollider>();
class TerrainCollider; template <> void RegisterClass<TerrainCollider>();
class WheelCollider; 
namespace Unity { class Joint; } 
namespace Unity { class CharacterJoint; } 
namespace Unity { class ConfigurableJoint; } 
namespace Unity { class FixedJoint; } 
namespace Unity { class HingeJoint; } 
namespace Unity { class SpringJoint; } 
class LODGroup; 
class MeshFilter; template <> void RegisterClass<MeshFilter>();
class OcclusionArea; 
class OcclusionPortal; 
class ParticleAnimator; 
class ParticleEmitter; 
class EllipsoidParticleEmitter; 
class MeshParticleEmitter; 
class ParticleSystem; 
class Renderer; template <> void RegisterClass<Renderer>();
class BillboardRenderer; 
class LineRenderer; 
class MeshRenderer; template <> void RegisterClass<MeshRenderer>();
class ParticleRenderer; 
class ParticleSystemRenderer; 
class SkinnedMeshRenderer; 
class SpriteMask; 
class SpriteRenderer; 
class TilemapRenderer; 
class TrailRenderer; 
class Rigidbody; template <> void RegisterClass<Rigidbody>();
class Rigidbody2D; 
namespace TextRenderingPrivate { class TextMesh; } 
class Transform; template <> void RegisterClass<Transform>();
namespace UI { class RectTransform; } template <> void RegisterClass<UI::RectTransform>();
class Tree; 
class WorldParticleCollider; 
class GameObject; template <> void RegisterClass<GameObject>();
class NamedObject; template <> void RegisterClass<NamedObject>();
class AssetBundle; 
class AssetBundleManifest; 
class ScriptedImporter; 
class AudioMixer; 
class AudioMixerController; 
class AudioMixerGroup; 
class AudioMixerGroupController; 
class AudioMixerSnapshot; 
class AudioMixerSnapshotController; 
class Avatar; 
class AvatarMask; 
class BillboardAsset; 
class ComputeShader; 
class Flare; 
namespace TextRendering { class Font; } template <> void RegisterClass<TextRendering::Font>();
class GameObjectRecorder; 
class LightProbes; 
class Material; template <> void RegisterClass<Material>();
class ProceduralMaterial; 
class Mesh; template <> void RegisterClass<Mesh>();
class Motion; template <> void RegisterClass<Motion>();
class AnimationClip; template <> void RegisterClass<AnimationClip>();
class PreviewAnimationClip; 
class NavMeshData; 
class OcclusionCullingData; 
class PhysicMaterial; 
class PhysicsMaterial2D; 
class PreloadData; template <> void RegisterClass<PreloadData>();
class RuntimeAnimatorController; 
class AnimatorController; 
class AnimatorOverrideController; 
class SampleClip; template <> void RegisterClass<SampleClip>();
class AudioClip; template <> void RegisterClass<AudioClip>();
class Shader; template <> void RegisterClass<Shader>();
class ShaderVariantCollection; 
class SpeedTreeWindAsset; 
class Sprite; template <> void RegisterClass<Sprite>();
class SpriteAtlas; 
class SubstanceArchive; 
class TerrainData; template <> void RegisterClass<TerrainData>();
class TextAsset; template <> void RegisterClass<TextAsset>();
class CGProgram; 
class MonoScript; template <> void RegisterClass<MonoScript>();
class Texture; template <> void RegisterClass<Texture>();
class BaseVideoTexture; 
class WebCamTexture; 
class CubemapArray; 
class LowerResBlitTexture; template <> void RegisterClass<LowerResBlitTexture>();
class ProceduralTexture; 
class RenderTexture; template <> void RegisterClass<RenderTexture>();
class CustomRenderTexture; 
class SparseTexture; 
class Texture2D; template <> void RegisterClass<Texture2D>();
class Cubemap; template <> void RegisterClass<Cubemap>();
class Texture2DArray; template <> void RegisterClass<Texture2DArray>();
class Texture3D; template <> void RegisterClass<Texture3D>();
class VideoClip; 
class GameManager; template <> void RegisterClass<GameManager>();
class GlobalGameManager; template <> void RegisterClass<GlobalGameManager>();
class AudioManager; template <> void RegisterClass<AudioManager>();
class BuildSettings; template <> void RegisterClass<BuildSettings>();
class CloudWebServicesManager; template <> void RegisterClass<CloudWebServicesManager>();
class CrashReportManager; 
class DelayedCallManager; template <> void RegisterClass<DelayedCallManager>();
class GraphicsSettings; template <> void RegisterClass<GraphicsSettings>();
class InputManager; template <> void RegisterClass<InputManager>();
class MonoManager; template <> void RegisterClass<MonoManager>();
class NavMeshProjectSettings; 
class PerformanceReportingManager; 
class Physics2DSettings; 
class PhysicsManager; template <> void RegisterClass<PhysicsManager>();
class PlayerSettings; template <> void RegisterClass<PlayerSettings>();
class QualitySettings; template <> void RegisterClass<QualitySettings>();
class ResourceManager; template <> void RegisterClass<ResourceManager>();
class RuntimeInitializeOnLoadManager; template <> void RegisterClass<RuntimeInitializeOnLoadManager>();
class ScriptMapper; template <> void RegisterClass<ScriptMapper>();
class TagManager; template <> void RegisterClass<TagManager>();
class TimeManager; template <> void RegisterClass<TimeManager>();
class UnityAnalyticsManager; 
class UnityConnectSettings; template <> void RegisterClass<UnityConnectSettings>();
class LevelGameManager; template <> void RegisterClass<LevelGameManager>();
class LightmapSettings; template <> void RegisterClass<LightmapSettings>();
class NavMeshSettings; 
class OcclusionCullingSettings; 
class RenderSettings; template <> void RegisterClass<RenderSettings>();
class RenderPassAttachment; 

void RegisterAllClasses()
{
void RegisterBuiltinTypes();
RegisterBuiltinTypes();
	//Total: 74 non stripped classes
	//0. Behaviour
	RegisterClass<Behaviour>();
	//1. Unity::Component
	RegisterClass<Unity::Component>();
	//2. EditorExtension
	RegisterClass<EditorExtension>();
	//3. Camera
	RegisterClass<Camera>();
	//4. GameObject
	RegisterClass<GameObject>();
	//5. QualitySettings
	RegisterClass<QualitySettings>();
	//6. GlobalGameManager
	RegisterClass<GlobalGameManager>();
	//7. GameManager
	RegisterClass<GameManager>();
	//8. Renderer
	RegisterClass<Renderer>();
	//9. GUIElement
	RegisterClass<GUIElement>();
	//10. GUITexture
	RegisterClass<GUITexture>();
	//11. GUILayer
	RegisterClass<GUILayer>();
	//12. MonoBehaviour
	RegisterClass<MonoBehaviour>();
	//13. Shader
	RegisterClass<Shader>();
	//14. NamedObject
	RegisterClass<NamedObject>();
	//15. Material
	RegisterClass<Material>();
	//16. Sprite
	RegisterClass<Sprite>();
	//17. Texture
	RegisterClass<Texture>();
	//18. Texture2D
	RegisterClass<Texture2D>();
	//19. RenderTexture
	RegisterClass<RenderTexture>();
	//20. Transform
	RegisterClass<Transform>();
	//21. UI::RectTransform
	RegisterClass<UI::RectTransform>();
	//22. Skybox
	RegisterClass<Skybox>();
	//23. Mesh
	RegisterClass<Mesh>();
	//24. Rigidbody
	RegisterClass<Rigidbody>();
	//25. Collider
	RegisterClass<Collider>();
	//26. CharacterController
	RegisterClass<CharacterController>();
	//27. Animator
	RegisterClass<Animator>();
	//28. TextRendering::Font
	RegisterClass<TextRendering::Font>();
	//29. UI::Canvas
	RegisterClass<UI::Canvas>();
	//30. UI::CanvasGroup
	RegisterClass<UI::CanvasGroup>();
	//31. UI::CanvasRenderer
	RegisterClass<UI::CanvasRenderer>();
	//32. AudioClip
	RegisterClass<AudioClip>();
	//33. SampleClip
	RegisterClass<SampleClip>();
	//34. AudioListener
	RegisterClass<AudioListener>();
	//35. AudioBehaviour
	RegisterClass<AudioBehaviour>();
	//36. AudioSource
	RegisterClass<AudioSource>();
	//37. Terrain
	RegisterClass<Terrain>();
	//38. TerrainData
	RegisterClass<TerrainData>();
	//39. FlareLayer
	RegisterClass<FlareLayer>();
	//40. PreloadData
	RegisterClass<PreloadData>();
	//41. Cubemap
	RegisterClass<Cubemap>();
	//42. Texture3D
	RegisterClass<Texture3D>();
	//43. Texture2DArray
	RegisterClass<Texture2DArray>();
	//44. MeshFilter
	RegisterClass<MeshFilter>();
	//45. MeshRenderer
	RegisterClass<MeshRenderer>();
	//46. LowerResBlitTexture
	RegisterClass<LowerResBlitTexture>();
	//47. MonoScript
	RegisterClass<MonoScript>();
	//48. TextAsset
	RegisterClass<TextAsset>();
	//49. BuildSettings
	RegisterClass<BuildSettings>();
	//50. DelayedCallManager
	RegisterClass<DelayedCallManager>();
	//51. GraphicsSettings
	RegisterClass<GraphicsSettings>();
	//52. InputManager
	RegisterClass<InputManager>();
	//53. PlayerSettings
	RegisterClass<PlayerSettings>();
	//54. ResourceManager
	RegisterClass<ResourceManager>();
	//55. RuntimeInitializeOnLoadManager
	RegisterClass<RuntimeInitializeOnLoadManager>();
	//56. ScriptMapper
	RegisterClass<ScriptMapper>();
	//57. TagManager
	RegisterClass<TagManager>();
	//58. TimeManager
	RegisterClass<TimeManager>();
	//59. MonoManager
	RegisterClass<MonoManager>();
	//60. PhysicsManager
	RegisterClass<PhysicsManager>();
	//61. AudioManager
	RegisterClass<AudioManager>();
	//62. UnityConnectSettings
	RegisterClass<UnityConnectSettings>();
	//63. CloudWebServicesManager
	RegisterClass<CloudWebServicesManager>();
	//64. Motion
	RegisterClass<Motion>();
	//65. LevelGameManager
	RegisterClass<LevelGameManager>();
	//66. Light
	RegisterClass<Light>();
	//67. LightmapSettings
	RegisterClass<LightmapSettings>();
	//68. RenderSettings
	RegisterClass<RenderSettings>();
	//69. BoxCollider
	RegisterClass<BoxCollider>();
	//70. SphereCollider
	RegisterClass<SphereCollider>();
	//71. TerrainCollider
	RegisterClass<TerrainCollider>();
	//72. Animation
	RegisterClass<Animation>();
	//73. AnimationClip
	RegisterClass<AnimationClip>();

}
