using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPReceiver : MonoBehaviour
{
	public int LOCA_LPORT = 22222;
	static UdpClient udp;
	static bool is_running = false;
	Thread thread;

	void Start()
	{
		udp = new UdpClient(LOCA_LPORT);
		udp.Client.ReceiveTimeout = 0;
		thread = new Thread(new ThreadStart(ThreadMethod));
		is_running = true;
		thread.Start();
	}

	void Update()
	{
	}

	void OnApplicationQuit()
	{
		is_running = false;
		thread.Abort();
		udp.Close();
		Debug.Log("UDPReceiver:closed");
	}

	private static void ThreadMethod()
	{
		while (is_running)
		{
			IPEndPoint remoteEP = null;
			byte[] data = udp.Receive(ref remoteEP);
			string text = Encoding.ASCII.GetString(data);
			Debug.Log(text);
		}
	}
}
