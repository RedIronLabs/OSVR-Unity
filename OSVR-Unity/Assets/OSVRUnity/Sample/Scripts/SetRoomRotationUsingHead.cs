/// OSVR-Unity
///
/// http://sensics.com/osvr
///
/// <copyright>
/// Copyright 2016 Sensics, Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///     http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
/// </copyright>

using UnityEngine;

namespace OSVR
{
    namespace Unity
    {
        /// <summary>
        /// Allows clearing or resetting the view based in relation to the HMD rotation
        /// </summary>
        public class SetRoomRotationUsingHead : MonoBehaviour
        {
            public KeyCode setRoomRotationKey = KeyCode.R;
            public KeyCode clearRoomRotationKey = KeyCode.U;
            public bool recenterOnPlay = true; 

            private ClientKit _clientKit;
            private DisplayController _displayController;

            void Awake()
            {
                _clientKit = ClientKit.instance;
                _displayController = FindObjectOfType<DisplayController>();

                if (recenterOnPlay)
                    SetRoomRotation();
            }

            void Update()
            {
                if (Input.GetKeyDown(setRoomRotationKey))
                    SetRoomRotation();

                if (Input.GetKeyDown(clearRoomRotationKey))
                    ClearRoomTransform();
            }

            /// <summary>
            /// Clears the offset below, mirroring the real-world geo rotation of the camera to the scene view
            /// </summary>
            private void ClearRoomTransform()
            {
                if (_displayController != null && _displayController.UseRenderManager)
                    _displayController.RenderManager.ClearRoomToWorldTransform();
                else
                    _clientKit.context.ClearRoomToWorldTransform();
            }

            /// <summary>
            /// Essentially clears the rotation of the view, essentially setting the 3D View's "forward" view to 0,0,0
            /// </summary>
            private void SetRoomRotation()
            {
                if (_displayController != null && _displayController.UseRenderManager)
                    _displayController.RenderManager.SetRoomRotationUsingHead();
                else
                    _clientKit.context.SetRoomRotationUsingHead();
            }
        }
    }
}