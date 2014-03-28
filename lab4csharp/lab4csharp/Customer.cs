﻿// Franklin Leung
// Lab 4 - COSC 3319
// Fall 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab4csharp
{
    class Customer
    {
        private String name;
        private String phone;

        public Customer(String name, String phone)
        {
            this.name = name;
            this.phone = phone;
        }
        public String getName()
        {
            return name;
        }
        public void setName(String newName)
        {
            this.name = newName;
        }
        public String getPhone()
        {
            return phone;
        }
        public void setPhone(String newPhone)
        {
            this.phone = newPhone;
        }
    }
}
