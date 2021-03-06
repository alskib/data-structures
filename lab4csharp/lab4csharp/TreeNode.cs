﻿// Franklin Leung
// Lab 4 - COSC 3319
// Fall 2012

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab4csharp
{
    class TreeNode
    {
        private TreeNode LLink, RLink;
        private bool LTag, RTag, visited;
        private Customer info;

        public TreeNode(Customer info)
        {
            this.info = info;
            LLink = this;
            LTag = false;
            RTag = false;
            RLink = this;
            visited = false;
        }

        public void setAllAttr(bool ltag, TreeNode llink, TreeNode rlink, bool rtag)
        {
            LTag = ltag;
            LLink = llink;
            RLink = rlink;
            RTag = rtag;
        }

        // LLink get/set
        public void setLLink(TreeNode link)
        {
            LLink = link;
        }

        public TreeNode getLLink()
        {
            return LLink;
        }

        // RLink get/set
        public void setRLink(TreeNode link)
        {
            RLink = link;
        }

        public TreeNode getRLink()
        {
            return RLink;
        }

        // LTag get/set
        public void setLTag(bool b)
        {
            LTag = b;
        }

        public bool getLTag()
        {
            return LTag;
        }

        // RTag get/set
        public void setRTag(bool b)
        {
            RTag = b;
        }

        public bool getRTag()
        {
            return RTag;
        }

        // visited get/set
        public void setVisited(bool v)
        {
            visited = v;
        }

        public bool getVisited()
        {
            return visited;
        }

        // both info get (output of data)
        public void visit() 
        {
            Console.WriteLine("{0} - {1}", this.info.getName(), this.info.getPhone());
        }

        public String getName()
        {
                return info.getName();
        }

        public String getPhone()
        {
            return this.info.getPhone();
        }

        // info set
        public void setInfo(String name, String phone)
        {
            if (name != null && phone != null)
            {
                this.info.setName(name);
                this.info.setPhone(phone);
            }
        }

        public bool isInfoNull()
        {
            if (this.getName() == null || this.getPhone() == null)
                return true;
            else
                return false;
        }
    }
}
