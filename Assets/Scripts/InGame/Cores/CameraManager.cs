using UnityEngine;

using Cinemachine;

namespace InGame.Cores
{
    public class CameraInfo
    {
        public Camera camera;
        public GameObject gameObject;
        public Transform transform;

        public CameraInfo(Camera cam)
        {
            this.camera = cam;
            this.gameObject = cam.gameObject;
            this.transform = cam.transform;
        }
    }

    public class CameraManager: MonoBehaviour
    {
        [Header("Camera Settings")]
        [SerializeField]
        private Camera[] m_cameras = new Camera[4];
        private CameraInfo m_board, m_boardThumbnail, m_battle, m_battleTest;
        
        [Header("Battle VCam Settings")]
        [SerializeField]
        private CinemachineVirtualCamera m_battleVirtualCamera;

        [SerializeField]
        private float[] battleBoundaries = new float[4];

        public void Awake()
        {
            m_board = new CameraInfo(m_cameras[0]);
            m_boardThumbnail = new CameraInfo(m_cameras[1]);
            m_battle = new CameraInfo(m_cameras[2]);
            m_battleTest = new CameraInfo(m_cameras[3]);
        }

        #region Getter
        public Camera boardCamera => m_board.camera;
        public Camera boardThumbnailCamera => m_boardThumbnail.camera;
        public Camera battleCamera => m_battle.camera;
        public Camera battleSimulationCamera => m_battleTest.camera;
        public Vector2 GetBattleSize()
        {
            float height = 2f * m_battle.camera.orthographicSize;
            float width = height * m_battle.camera.aspect;
            return new Vector2(width, height);
        }
        #endregion

        #region Setter
        public void SetBoardActive(bool active) => m_board.gameObject.SetActive(active);
        public void SetBoardThumbnailActive(bool active) => m_boardThumbnail.gameObject.SetActive(active);
        public void SetBoardThumbnailSize(float size) => m_boardThumbnail.camera.orthographicSize = size;
        public void SetBattleActive(bool active) => m_battle.gameObject.SetActive(active);
        public void SetBattleCameraFollow(GameObject target) => m_battleVirtualCamera.Follow = target?.transform;
        
        public void SetBoardCameraPosition(Vector2 pos)
        {
            var z = m_board.transform.position.z;
            var position = new Vector3(pos.x, pos.y, z);
            m_board.transform.position = position;
        }
        
        public void SetMiniBoardCameraPosition(Vector2 pos)
        {
            var z = m_boardThumbnail.transform.position.z;
            var position = new Vector3(pos.x, pos.y, z);
            m_boardThumbnail.transform.position = position;
        }
        #endregion
        
        #region Update
        public void UpdateBattlePosition()
        {
            Vector3 position = m_battle.transform.position;
            float height = m_battle.camera.orthographicSize;
            float width = height * m_battle.camera.aspect;
            
            float clampedX = Mathf.Clamp(position.x, 
                                        battleBoundaries[0] + width, 
                                        battleBoundaries[1] - width);
            float clampedY = Mathf.Clamp(position.y, 
                                        battleBoundaries[2] + height, 
                                        battleBoundaries[3] - height);
            
            m_battle.transform.position = new Vector3(clampedX, clampedY, 
                                                    m_battle.transform.position.z);
            
            bool atEdgeX = m_battle.transform.position.x == clampedX;
            bool atEdgeY = m_battle.transform.position.y == clampedY;

            if (atEdgeX || atEdgeY) SetBattleCameraFollow(null);
            else SetBattleCameraFollow(GameManager.Instance.GetAndroid().androidView?.gameObject);
        }
        #endregion
    }
}