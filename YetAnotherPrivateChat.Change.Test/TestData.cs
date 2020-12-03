using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;
using System.Collections;
using Bogus;

namespace YetAnotherPrivateChat.Change.Test
{

    public class AddRoomTestData : TheoryData<string, bool>
    {
        public AddRoomTestData()
        {
            Add("Test", true);
            Add("TESASDJZXC", true);
            Add("Test!@$@#*$%$$$", true);
            Add("", false);
        }
    }

    public class EditRoomTestData : TheoryData<string, int, bool>
    {
        public EditRoomTestData()
        {
            Add("TestEdit", 0, true);
            Add("TESASDJZXCEdit", 1, true);
            Add("Test!@$@#*$%$$$Edit", 2, true);
            Add("Edit", 3, false);
        }
    }

    public class CloseOpenRoomTestData : TheoryData<int, bool>
    {
        public CloseOpenRoomTestData()
        {
            Add(0, true);
            Add(1, true);
            Add(2, true);
            Add(3, false);
        }
    }

    public class AddMessageTestData : TheoryData<string, int, bool>
    {
        public AddMessageTestData()
        {
            for (int f = 0; f < 3; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Add(string.Concat("test message", i.ToString()), f, true);
                }
            }

            Add("", 0, false);
            Add("", 1, false);
            Add("", 2, false);
        }
    }

    public class StarUnstarMessageTestData : TheoryData<int, bool>
    {
        public StarUnstarMessageTestData()
        {
            var msgId = -1;
            for (int f = 0; f < 3; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Add(msgId++, true);
                }
            }
        }
    }

     public class EditMessageTestData : TheoryData<int, bool>
    {
        public EditMessageTestData()
        {
            var msgId = -1;
            for (int f = 0; f < 3; f++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    Add(msgId++, true);
                }
            }
        }
    }

}

