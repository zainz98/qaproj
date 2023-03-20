﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_mstest
{
    public class Reader
    {
        #region Enums
        public enum ReaderType
        {
            Child,
            Adult,
        }
        #endregion

        #region Private Variables
        private string firstName;
        private string lastName;
        private ReaderType type;
        #endregion

        #region Properties
        public string FirstName 
        { 
            get { return firstName; }
        }
        public string LastName
        {
            get { return lastName; }
        }
        public ReaderType Type
        {
            get { return type; }
        }
        #endregion

        #region Constructors
        public Reader(string firstName,string lastName,ReaderType type)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.type = type;
        }

        #endregion
    }
}
