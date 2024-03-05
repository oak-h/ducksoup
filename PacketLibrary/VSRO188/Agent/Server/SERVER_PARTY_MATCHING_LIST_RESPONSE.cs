using PacketLibrary.VSRO188.Agent.Objects.Party;
using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Server;

// https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/AGENT_PARTY_MATCHING_LIST
public class SERVER_PARTY_MATCHING_LIST_RESPONSE : Packet
{
    public ushort ErrorCode;
    public byte PageCount;
    public byte PageIndex;
    public byte PartyCount;
    public List<PartyMatchEntry> PartyMatch = new();
    public byte Result;

    public SERVER_PARTY_MATCHING_LIST_RESPONSE() : base(0xB06C)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Server;

    public override PacketDirection ToDirection => PacketDirection.Client;


    public override async Task Read()
    {
        TryRead<byte>(out Result);
        switch (Result)
        {
            case 1:
            {
                TryRead<byte>(out PageCount);
                TryRead<byte>(out PageIndex);
                TryRead<byte>(out PartyCount);
                for (var i = 0; i < PartyCount; i++) PartyMatch.Add(new PartyMatchEntry(this));
                break;
            }
            case 2:
                TryRead<ushort>(out ErrorCode);
                break;
        }
    }

    public override async Task<Packet> Build()
    {
        Reset();
        TryWrite<byte>(Result);
        switch (Result)
        {
            case 1:
            {
                TryWrite<byte>(PageCount);
                TryWrite<byte>(PageIndex);
                TryWrite<byte>(PartyCount);
                foreach (var partyMatchEntry in PartyMatch) partyMatchEntry.Build(this);
                break;
            }
            case 2:
                TryWrite<ushort>(ErrorCode);
                break;
        }

        return this;
    }

    public static Packet of()
    {
        return new SERVER_PARTY_MATCHING_LIST_RESPONSE();
    }
}