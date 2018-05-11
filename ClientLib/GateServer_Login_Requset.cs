using Protocol.Client.Gate;

namespace ClientLib
{
    public partial class GateServer
    {
        public void Request_Test()
        {
            C2GTest1 msg = new C2GTest1();
            msg.Testint1 = 111;
            msg.Teststring1 = "aaa";
            Send(msg);
        }

        //public void Request_MSG_C2G_GET_ENCRYPTKEY()
        //{
        //    MSG_C2G_GetEncryptKey msg = new MSG_C2G_GetEncryptKey();
        //    Send(msg);
        //}

        //public void Login_Request_MSG_CG_USER_LOGIN()
        //{
        //    //Protocol.m_isLinkOverTime = false;
        //    MSG_CG_USER_LOGIN MSG_CG_USER_LOGIN = new MSG_CG_USER_LOGIN();
        //    string registerId = "";
        //    MSG_CG_USER_LOGIN.deviceId = "19e22d35f4a60df695d1a4d847992a85da077f35";
        //    MSG_CG_USER_LOGIN.registerId = registerId;
        //    MSG_CG_USER_LOGIN.accountName = PLAYER.AccountName;
        //    MSG_CG_USER_LOGIN.mainId = PLAYER.GameServerID;
        //    MSG_CG_USER_LOGIN.version = CFG.Version;
        //    Net.Send(MSG_CG_USER_LOGIN);
        //}

        //public void Login_Request_MSG_CG_TO_ZONE(int character_uid)
        //{
        //    MSG_CG_TO_ZONE MSG_CG_TO_ZONE = new MSG_CG_TO_ZONE();
        //    MSG_CG_TO_ZONE.uid = character_uid;
        //    Net.Send(MSG_CG_TO_ZONE);
        //}

        //public void Login_Request_MSG_CG_MAP_LOADING_DONE(int mapId, int channel)
        //{
        //    MSG_CG_MAP_LOADING_DONE MSG_CG_MAP_LOADING_DONE = new MSG_CG_MAP_LOADING_DONE();
        //    MSG_CG_MAP_LOADING_DONE.mapId = mapId;
        //    MSG_CG_MAP_LOADING_DONE.channel = channel;
        //    Net.Send(MSG_CG_MAP_LOADING_DONE);
        //    PLAYER.IsLogin = true;
        //}

    }
}