using System;

[Serializable]
public class GeneratedClient 
{
    public long timeGenerated;
    public Client client = null;

    public GeneratedClient() { }

    public GeneratedClient(long timeGenerated, Client client)
    {
        this.timeGenerated = timeGenerated;
        this.client = client;
    }
}