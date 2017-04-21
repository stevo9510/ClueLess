using System;

/// <summary>
/// Base Server to Client Message.  
/// 
/// This was originally going to hold a "MessageId" (StandardEnums.ServerToClientMessageEnum) 
/// that each message defines, but it may not be necessary for Socket.IO.  Leaving for now in case needed.
/// </summary>
[Serializable]
public abstract class BaseServerToClientMessage
{
}
