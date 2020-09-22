using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Network;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Bootstrap : MonoBehaviour
    {
        public Button Button;
        public string Name;
        public string Address;
        public int Port;

        private void Start()
        {
            Button.onClick.AddListener(() =>
            {
                var session = new Session(this.Name);

                var connection = new ClientConnection(session);
                connection.Connect(this.Address, this.Port);
            });
        }
    }
}
