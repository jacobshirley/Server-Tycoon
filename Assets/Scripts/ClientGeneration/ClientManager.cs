using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ClientManager
{
    public List<Client> clients = new List<Client>();

    public int AddClient(Client client)
    {
        this.clients.Add(client);

        return this.clients.Count - 1;
    }

    public Client GetClient(int id)
    {
        foreach (Client cl in clients)
        {
            if (cl.id == id)
                return cl;
        }

        return null;
    }

    public bool RemoveClient(Client client)
    {
        return this.clients.Remove(client);
    }

    public Client RemoveClient(int id)
    {
        foreach (Client cl in clients)
        {
            if (cl.id == id)
            {
                clients.Remove(cl);
                return cl;
            }
        }

        return null;
    }

    public List<Client> GetClients()
    {
        return clients;
    }
}
