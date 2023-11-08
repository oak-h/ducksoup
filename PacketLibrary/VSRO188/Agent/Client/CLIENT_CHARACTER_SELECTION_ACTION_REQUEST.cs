using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Client;

public class CLIENT_CHARACTER_SELECTION_ACTION_REQUEST : Packet
{
    public CLIENT_CHARACTER_SELECTION_ACTION_REQUEST() : base(0x7007)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Client;

    public override PacketDirection ToDirection => PacketDirection.Server;


    public override async Task Read()
    {
        //throw new NotImplementedException();
    }

    public override async Task<Packet> Build()
    {
        //throw new NotImplementedException();

        //Reset();

        return this;
    }

    public static Packet of()
    {
        return new CLIENT_CHARACTER_SELECTION_ACTION_REQUEST();
    }
}