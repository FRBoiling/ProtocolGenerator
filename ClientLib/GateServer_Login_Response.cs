//using CryptoLib;
//using Engine.Foundation;
//using GenerateCodeLib;
//using LogLib;
//using Protocol.Gate.G2C;
//using System;
//using System.IO;

//namespace ClientLib
//{
//    public partial class GateServer
//    {
//        public void BindResponse_Login()
//        {
//            AddProcesser(Id<MSG_G2C_EncryptKey>.Value, OnResponse_MSG_G2C_EncryptKey);
//            //Net.AddResponser(Id<MSG_GC_TIME_SYNC>.Value, OnResponse_MSG_GC_TIME_SYNC);
//            //Net.AddResponser(Id<MSG_GC_USER_LOGIN>.Value, OnResponse_MSG_GC_USER_LOGIN);
//            //Net.AddResponser(Id<MSG_GC_ENTER_WORLD>.Value, OnResponse_MSG_GC_ENTER_WORLD);
//            //Net.AddResponser(Id<MSG_GC_ENTER_ZONE>.Value, OnResponse_MSG_GC_ENTER_ZONE);
//        }

//        private static string _publicKey;

//        static GateServer()
//        {
//            try
//            {
//                FileStream fsRead = new FileStream(@"..\Data\Key\PublicKey.key", FileMode.Open, FileAccess.Read);
//                StreamReader sr = new StreamReader(fsRead);
//                _publicKey = sr.ReadLine();

//                sr.Close();
//                fsRead.Close();
//                Log.Info("PublicKey set success");
//            }
//            catch (Exception e)
//            {
//                Log.Error("PublicKey set error:" + e.ToString());
//            }
//        }



//        public void OnResponse_MSG_G2C_EncryptKey(MemoryStream stream, int uid = 0)
//        {
//            MSG_G2C_EncryptKey MSG_G2C_EncryptKey = ProtoBuf.Serializer.Deserialize<MSG_G2C_EncryptKey>(stream);
//            Parser.Parse(MSG_G2C_EncryptKey);
//            string encryptKey = RSAHelper.DecryptString(MSG_G2C_EncryptKey.EncryptKey, _publicKey);

//            SetBlowFish(new BlowFish(encryptKey));


//        }

//        //public void OnResponse_MSG_GC_TIME_SYNC(MemoryStream stream)
//        //{
//        //    MSG_GC_TIME_SYNC MSG_GC_TIME_SYNC = ProtoBuf.Serializer.Deserialize<MSG_GC_TIME_SYNC>(stream);
//        //    Parser.Parse(MSG_GC_TIME_SYNC);

//        //    //RefreshTimer.inst.ServerTime = RefreshTimer.inst.StampToDateTime((long)msg.timeStamp);
//        //    //RefreshTimer.inst.GameSyncTime = DateTime.Now;
//        //    //LuaManager.inst.Call("TimeManager.SetServerTime", LuaManager.GetMainState()["TimeManager"], msg.timeStamp.ToString());
//        //}


//        //List<MSG_GC_CHARACTER_INFO> mCharacterList = new List<MSG_GC_CHARACTER_INFO>();
//        //public void OnResponse_MSG_GC_USER_LOGIN(MemoryStream stream)
//        //{
//        //    MSG_GC_USER_LOGIN MSG_GC_USER_LOGIN = ProtoBuf.Serializer.Deserialize<MSG_GC_USER_LOGIN>(stream);
//        //    Parser.Parse(MSG_GC_USER_LOGIN);

//        //    if (MSG_GC_USER_LOGIN.result == 1)
//        //    {
//        //        mCharacterList.Clear();
//        //        for (int i = 0; i < MSG_GC_USER_LOGIN.character_list.Count; ++i)
//        //        {
//        //            MSG_GC_CHARACTER_INFO characterInfo = MSG_GC_USER_LOGIN.character_list[i];
//        //            mCharacterList.Add(characterInfo);
//        //        }
//        //        if (mCharacterList.Count > 0)
//        //        {
//        //            //跳过选角 TODO
//        //            Login_Request_MSG_CG_TO_ZONE(mCharacterList[0].Uid);
//        //        }
//        //        else
//        //        {
//        //            //UnityEngine.Debug.Log("Create new player");
//        //            //GameManager.inst.ChangeScene(DEF.SCENE_CREATECHARACTER);
//        //        }
//        //        //CFG_SAVE.SaveConfig();

//        //    }
//        //    else if (MSG_GC_USER_LOGIN.result == 144)
//        //    {
//        //        //EventManager.Dispatch(EventConst.BadVersion);
//        //    }
//        //    else
//        //    {
//        //        //UnityEngine.Debug.Log("Login failed: {0}" + MSG_GC_USER_LOGIN.result.ToString());
//        //    }
//        //    //UI.CloseWait();
//        //}
//        //public void OnResponse_MSG_GC_ENTER_WORLD(MemoryStream stream)
//        //{
//        //    MSG_GC_ENTER_WORLD MSG_GC_ENTER_WORLD = ProtoBuf.Serializer.Deserialize<MSG_GC_ENTER_WORLD>(stream);
//        //    Parser.Parse(MSG_GC_ENTER_WORLD);

//        //    PLAYER.CharInfo = MSG_GC_ENTER_WORLD.myselfInfo;
//        //    PLAYER.CurrentCharacterUid = MSG_GC_ENTER_WORLD.myselfInfo.Uid;
//        //    PLAYER.EquipPetID = MSG_GC_ENTER_WORLD.myselfInfo.PetId;
//        //    PLAYER.Token = MSG_GC_ENTER_WORLD.Token;
//        //    PLAYER.InFindingTarget = false;
//        //}
//        //public void OnResponse_MSG_GC_ENTER_ZONE(MemoryStream stream)
//        //{
//        //    MSG_GC_ENTER_ZONE MSG_GC_ENTER_ZONE = ProtoBuf.Serializer.Deserialize<MSG_GC_ENTER_ZONE>(stream);
//        //    Parser.Parse(MSG_GC_ENTER_ZONE);

//        //    PLAYER.MapTransforming = true;
//        //    PLAYER.CharInfo.InstanceId = MSG_GC_ENTER_ZONE.instanceId;
//        //    PLAYER.CurrentCharacterId = MSG_GC_ENTER_ZONE.instanceId;
//        //    PLAYER.CharInfo.PosX = MSG_GC_ENTER_ZONE.posX;
//        //    PLAYER.CharInfo.PosY = MSG_GC_ENTER_ZONE.posY;
//        //    PLAYER.IsChangeChannel = false;
//        //    if (PLAYER.Channel != 0 && PLAYER.Channel != MSG_GC_ENTER_ZONE.channel)
//        //    { //判断是否切线，是则不load场景 不清除NPC和采集物 TODO
//        //        PLAYER.IsChangeChannel = true;
//        //    }
//        //    PLAYER.Channel = MSG_GC_ENTER_ZONE.channel;
//        //    PLAYER.MinChannel = MSG_GC_ENTER_ZONE.minChannel;
//        //    PLAYER.MaxChannel = MSG_GC_ENTER_ZONE.maxChannel;
//        //    if (PLAYER.BattleType < (int)BattleType.MatchNormal)
//        //    {//非战场
//        //        if (!PLAYER.IsChangeChannel)
//        //        {//非切线
//        //            //CharacterManager.inst.ClearQueue();
//        //            //CharacterManager.inst.BindPlayer(PLAYER.CharInfo);
//        //            //MapManager.inst.LoadMap(MSG_GC_ENTER_ZONE.mapId);
//        //            //foreach (MSG_ZGC_NPC_INFO info in MSG_GC_ENTER_ZONE.NpcList)
//        //            //{
//        //            //    CharacterManager.inst.BindNPC(info);
//        //            //}
//        //            //foreach (MSG_ZGC_GOODS_INFO item in MSG_GC_ENTER_ZONE.GoodsList)
//        //            //{
//        //            //    CharacterManager.inst.BindNPCItem(item);
//        //            //}
//        //            Login_Request_MSG_CG_MAP_LOADING_DONE(MSG_GC_ENTER_ZONE.mapId, PLAYER.Channel);
//        //        }
//        //        else
//        //        {//切线 清除场景中玩家
//        //            //CharacterManager.inst.ClearOtherPlayer();
//        //            //// CharacterManager.inst.ClearQueue();
//        //            //CharacterManager.inst.BindPlayer(PLAYER.CharInfo);
//        //            //UI.CloseWait(OnCloseWait(), 3f, "system_changechannelsuccess");
//        //            Login_Request_MSG_CG_MAP_LOADING_DONE(MSG_GC_ENTER_ZONE.mapId, PLAYER.Channel);
//        //            PLAYER.MapTransforming = false;
//        //            //UnityEngine.Debug.Log("===PLAYER.Channel=== 切线成功" + PLAYER.Channel);
//        //        }
//        //    }
//        //    else
//        //    {//战场
//        //        //CharacterManager.inst.ClearQueue();
//        //        //CharacterManager.inst.BindPlayer(PLAYER.CharInfo);
//        //        Login_Request_MSG_CG_MAP_LOADING_DONE(MSG_GC_ENTER_ZONE.mapId, PLAYER.Channel);
//        //    }
//        //    //PLAYER.CurrentActorInfo.lookAngle = MSG_GC_ENTER_ZONE.angle;
//        //    //return true;
//        //}
//    }
//}