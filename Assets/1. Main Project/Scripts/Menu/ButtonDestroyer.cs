using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ButtonDestroyer : MonoBehaviour
{
    public GameObject objetoADestruir;
    public PhotonView photonView;

    public void DestroyObject()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsConnectedAndReady)
        {
            // Llamar RPC para destruir objeto en otros clientes
            photonView.RPC("DestroyObjectRPC", RpcTarget.OthersBuffered);
        }
        else
        {
            // Destruir objeto localmente si no estás en una red
            Destroy(objetoADestruir);
        }
    }

    [PunRPC]
    public void DestroyObjectRPC()
    {
        // Destruir objeto en otros clientes
        Destroy(objetoADestruir);
    }
}



