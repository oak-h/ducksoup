using PacketLibrary.VSRO188.Agent.Objects.Party;
using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Server;

// https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/AGENT_PARTY_MATCHING_PLAYER_JOIN
public class SERVER_PARTY_MATCHING_PLAYER_JOIN_REQUEST : Packet
{
    public uint RequestID;
    public uint UserJID;
    public uint MatchingID;
    public uint PrimaryMastery;
    public uint SecondaryMastery;
    public byte JobState;
    public PartyMemberInfo memberInfo;

    public SERVER_PARTY_MATCHING_PLAYER_JOIN_REQUEST() : base(0x706D)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Server;

    public override PacketDirection ToDirection => PacketDirection.Client;


    public override async Task Read()
    {
        TryRead(out RequestID);
        TryRead(out UserJID);
        TryRead(out MatchingID);
        TryRead(out PrimaryMastery);
        TryRead(out SecondaryMastery);
        TryRead(out JobState);
        PartyMemberInfo memberInfo = new PartyMemberInfo(this);
    }

    public override async Task<Packet> Build()
    {
        Reset();
        TryWrite(RequestID);
        TryWrite(UserJID);
        TryWrite(MatchingID);
        TryWrite(PrimaryMastery);
        TryWrite(SecondaryMastery);
        TryWrite(JobState);
        memberInfo.Build(this);
        return this;
    }

    public static Packet of()
    {
        return new SERVER_PARTY_MATCHING_PLAYER_JOIN_REQUEST();
    }
}