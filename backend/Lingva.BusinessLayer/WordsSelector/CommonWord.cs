﻿using System;
using System.Collections.Generic;
using Lingva.BusinessLayer.Interfaces;

namespace Lingva.BusinessLayer.WordsSelector
{
    public class CommonWord : ICommonWord
    {
        public bool AreExist(string word)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllSimpleWords()
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserDictionary(int userId)
        {
            throw new NotImplementedException();
        }
    }
}