using PacketLibrary.VSRO188.Agent.Enums.Chat;
using SilkroadSecurityAPI.Message;

namespace PacketLibrary.VSRO188.Agent.Server;

// https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/AGENT_CHAT
public class SERVER_CHAT_RESPONSE : Packet
{
    public byte ChatIndex;
    public ChatType ChatType;
    public ChatErrorCode ErrorCode;


    public byte Result; // 1 byte result

    public SERVER_CHAT_RESPONSE() : base(0xB025)
    {
    }

    public override PacketDirection FromDirection => PacketDirection.Server;
    public override PacketDirection ToDirection => PacketDirection.Client;

    public override async Task Read()
    {
        TryRead(out Result);
        if (Result == 0x2)
        {
            TryRead(out ErrorCode);
        }
        TryRead(out ChatType);
        TryRead(out ChatIndex);
    }

    public override async Task<Packet> Build()
    {
        Reset();

        TryWrite(Result);
        if (Result == 0x02)
        {
            TryWrite(ErrorCode);
        }

        TryWrite(ChatType);
        TryWrite(ChatIndex);

        return this;
    }

    public static Task<Packet> of(byte result, ChatErrorCode errorCode, ChatType chatType, byte chatIndex)
    {
        return new SERVER_CHAT_RESPONSE
        {
            Result = result,
            ErrorCode = errorCode,
            ChatType = chatType,
            ChatIndex = chatIndex
        }.Build();
    }

    public static Task<Packet> of(byte result, ChatType chatType, byte chatIndex)
    {
        return new SERVER_CHAT_RESPONSE
        {
            Result = result,
            ChatType = chatType,
            ChatIndex = chatIndex
        }.Build();
    }
}