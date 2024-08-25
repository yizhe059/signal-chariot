using UnityEngine;

using Cinemachine;

namespace InGame.Cameras
{
    public class CameraManager: MonoBehaviour
    {
        [SerializeField]
        private Camera m_boardCamera, m_boardThumbnailCamera, m_battleCamera, m_battleSimulationCamera;
        
        [SerializeField]
        private CinemachineVirtualCamera m_battleVirtualCamera;

        public Camera boardCamera => m_boardCamera;
        public Camera boardThumbnailCamera => m_boardThumbnailCamera;
        public Camera battleCamera => m_battleCamera;
        public Camera battleSimulationCamera => m_battleSimulationCamera;

        public void BoardCameraSetActive(bool active) => m_boardCamera.gameObject.SetActive(active);
        
        public void MiniBoardCameraSetActive(bool active) => m_boardThumbnailCamera.gameObject.SetActive(active);

        public void BattleCameraSetActive(bool active) => m_battleCamera.gameObject.SetActive(active);

        public void SetMiniBoardCameraSize(float size) => m_boardThumbnailCamera.orthographicSize = size;

        public void SetBoardCameraPosition(Vector2 pos)
        {
            var z = m_boardCamera.gameObject.transform.position.z;
            var position = new Vector3(pos.x, pos.y, z);
            m_boardCamera.transform.position = position;
        }
        
        public void SetMiniBoardCameraPosition(Vector2 pos)
        {
            var z = m_boardThumbnailCamera.gameObject.transform.position.z;
            var position = new Vector3(pos.x, pos.y, z);
            m_boardThumbnailCamera.transform.position = position;
        }

        public void SetBattleCameraFollow(GameObject target)
        {
            if(target == null){
                Debug.LogError("Can't find the follow target of Battle Virtual Camera!");
                return;
            }
            m_battleVirtualCamera.Follow = target.transform;
        }

        public Vector2 GetBattleCameraSize()
        {
            float height = 2f * m_battleCamera.orthographicSize;
            float width = height * m_battleCamera.aspect;
            return new Vector2(width, height);
        }
        
        // no set position for battle camera because battle camera is using virtual camera
    }
}