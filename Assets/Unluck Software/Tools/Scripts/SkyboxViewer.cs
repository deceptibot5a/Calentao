namespace UnluckSoftware
{
	using UnityEngine;
	public class SkyboxViewer :MonoBehaviour
	{
		public Material[] _skyboxes;
		public bool _repeat = true;
		int _skyboxIndex = -1;

		void Update()
		{
			if (Input.GetKeyUp("n")) NextSkybox();
			if (Input.GetKeyUp("b")) PrevSkybox();
		}
		void NextSkybox()
		{
			_skyboxIndex++;
			if (_skyboxIndex > _skyboxes.Length - 1)
			{
				if (!_repeat) return;
				_skyboxIndex = 0;
			}
			ChangeSkybox();
		}
		void PrevSkybox()
		{
			_skyboxIndex--;
			if (_skyboxIndex < 0)
			{
				if (!_repeat) return;
				_skyboxIndex = _skyboxes.Length - 1;
			}
			ChangeSkybox();
		}
		void ChangeSkybox()
		{
			RenderSettings.skybox = _skyboxes[_skyboxIndex];
			Debug.Log(_skyboxes[_skyboxIndex].name);
		}
	}
}
