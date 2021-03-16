using ModestTree;
using UnityEngine;
using Zenject;

namespace Assets.Scripts
{
	[CreateAssetMenu(menuName = "ScriptableObject/DefaultSceneInstaller")]
	public class DefaultSceneInstaller : ScriptableObjectInstaller<DefaultSceneInstaller>
	{
		//[SerializeField] private GameObject mainCamera;

		public override void InstallBindings()
		{
			//Container.Bind<PlayerInputHandler>().FromComponentInNewPrefab(playerInputHandler).AsSingle().NonLazy();

			Container.BindFactory<GameObject, Transform, PrefabFactory>().FromFactory<NormalPrefabFactory>();
		}
	}

	public class PrefabFactory : PlaceholderFactory<GameObject, Transform>
	{
		public Transform Create(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
		{
			var t = Create(prefab);
			t.SetParent(parent, worldPositionStays: false);
			t.position = position;
			t.rotation = rotation;
			return t;
		}
		public Transform Create(GameObject prefab, Transform parent)
		{
			var t = Create(prefab);
			t.SetParent(parent, worldPositionStays: false);
			return t;
		}
	}

	public class NormalPrefabFactory : IFactory<GameObject, Transform>
	{
		[Inject]
		readonly DiContainer _container = null;

		public Transform Create(GameObject prefab)
		{
			Assert.That(prefab != null, "Null prefab given to factory create method");

			return _container.InstantiatePrefab(prefab).transform;
		}
	}
}
