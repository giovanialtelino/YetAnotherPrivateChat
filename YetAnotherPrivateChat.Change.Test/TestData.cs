using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;
using System.Collections;
using Bogus;
using YetAnotherPrivateChat.Shared.HelperShared.JWT;

namespace YetAnotherPrivateChat.Change.Test
{
    public class AddRoomTestData : TheoryData<string, JwtToken, bool>
    {
        public AddRoomTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);

            Add("Test", jwtAdmin, true);
            Add("Test", jwtUser, false);
            Add("TESASDJZXC", jwtAdmin, true);
            Add("Test!@$@#*$%$$$", jwtAdmin, true);
            Add("", jwtAdmin, false);
        }
    }

    public class EditRoomTestData : TheoryData<string, int, JwtToken, bool>
    {
        public EditRoomTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);

            Add("TestEdit", 1, jwtAdmin, true);
            Add("TESASDJZXCEdit", 2, jwtAdmin, true);
            Add("Test!@$@#*$%$$$Edit", 3, jwtAdmin, true);
            Add("Edit", 4, jwtAdmin, false);
            Add("Edit", 1, jwtUser, false);
        }
    }

    public class CloseOpenRoomTestData : TheoryData<int, JwtToken, bool>
    {
        public CloseOpenRoomTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);

            Add(1, jwtAdmin, true);
            Add(2, jwtAdmin, true);
            Add(3, jwtAdmin, true);
            Add(4, jwtAdmin, false);
            Add(1, jwtUser, false);
            Add(3, jwtUser, false);
        }
    }

    public class AddMessageTestData : TheoryData<string, int, JwtToken, bool>
    {
        public AddMessageTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);

            for (int f = 1; f < 4; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        Add(string.Concat("test message", i.ToString()), f, jwtAdmin, true);
                    }
                    else
                    {
                        Add(string.Concat("test message", i.ToString()), f, jwtUser, true);
                    }
                }
            }

            Add("", 1, jwtUser, false);
            Add("", 2, jwtUser, false);
            Add("", 3, jwtUser, false);
            Add("", 1, jwtAdmin, false);
            Add("", 2, jwtAdmin, false);
            Add("", 3, jwtAdmin, false);
        }
    }

    public class StarUnstarMessageTestData : TheoryData<int, JwtToken, bool>
    {
        public StarUnstarMessageTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);
            var msgId = 1;
            for (int f = 1; f < 4; f++)
            {
                for (int i = 1; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        Add(msgId++, jwtUser, true);
                    }
                    else
                    {
                        Add(msgId++, jwtAdmin, true);
                    }
                }
            }
        }
    }

    public class EditMessageTestData : TheoryData<int, string, JwtToken, bool>
    {
        public EditMessageTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);
            var msgId = 1;

            for (int f = 1; f < 4; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        Add(msgId, "edit message --- " + i, jwtAdmin, true);
                        Add(msgId, "edit message --- " + i, jwtUser, false);
                    }
                    else
                    {
                        Add(msgId, "edit message --- " + i, jwtUser, true);
                        Add(msgId, "edit message --- " + i, jwtAdmin, false);
                    }
                    msgId++;
                }
            }

        }
    }

    public class DeleteMessageTestData : TheoryData<int, JwtToken, bool>
    {
        public DeleteMessageTestData()
        {
            var jwtAdmin = new JwtToken(1, DateTime.Now, 1);
            var jwtUser = new JwtToken(2, DateTime.Now, 0);
            var msgId = 1;

            for (int f = 1; f < 4; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i % 2 == 0)
                    {
                        Add(msgId, jwtAdmin, true);
                        Add(msgId, jwtUser, false);
                    }
                    else
                    {
                        Add(msgId, jwtUser, true);
                        Add(msgId, jwtAdmin, false);
                    }
                    msgId++;
                }
            }

        }
    }

}

