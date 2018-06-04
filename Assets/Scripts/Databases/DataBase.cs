using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase
{
    private static ItemDatabase m_ItemDatabase = Resources.Load<ItemDatabase>("Databases/ItemDatabase");
    public static ItemDatabase Item
    {
        get
        {
            if(m_ItemDatabase == null)
            {
                m_ItemDatabase = Resources.Load< ItemDatabase>("Databases/ItemDatabase");
            }
            return m_ItemDatabase;
        }
    }

}
