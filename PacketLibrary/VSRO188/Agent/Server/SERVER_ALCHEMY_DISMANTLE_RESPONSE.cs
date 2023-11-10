using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Server;

public class SERVER_ALCHEMY_DISMANTLE_RESPONSE : Packet
{
    public byte Result;
    public ushort ErrorCode;
    public SERVER_ALCHEMY_DISMANTLE_RESPONSE() : base(0xB157)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Server;

    public override PacketDirection ToDirection => PacketDirection.Client;


    public override async Task Read()
    {
        TryRead(out Result);
        if (Result == 0x02)
        {
            TryRead(out ErrorCode);
        }
    }

    public override async Task<Packet> Build()
    {
        Reset();
        TryWrite(Result);
        if (Result == 0x02)
        {
            TryWrite(ErrorCode);
        }
        return this;
    }

    public static Packet of()
    {
        return new SERVER_ALCHEMY_DISMANTLE_RESPONSE();
    }
}