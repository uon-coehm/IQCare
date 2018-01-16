using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DataAccess.Base;
using DataAccess.Common;
using DataAccess.Entity;
using Interface.SCM;
using Application.Common;
using System.Collections;

namespace BusinessProcess.SCM
{

    public class BMasterList : ProcessBase, IMasterList
    {
        #region "Constructor"

        public BMasterList()
        {
        }

        #endregion

        ClsUtility oUtility = new ClsUtility();

        public DataSet GetProgramList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject Programlist = new ClsObject();
                return
                    (DataSet)
                    Programlist.ReturnObject(oUtility.theParams, "pr_SCM_GetProgramList_Futures",
                                             ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet SaveBatchName(string BatchName, int UserId,string itemID,string expiryDatetime)
        {
            lock (this)
            {
                ClsObject BatchNameMgr = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@BatchName", SqlDbType.VarChar, BatchName.ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                oUtility.AddParameters("@ItemID", SqlDbType.VarChar, itemID.ToString());
                oUtility.AddParameters("@ExpiryDatetime", SqlDbType.VarChar, expiryDatetime.ToString());

                return
                    (DataSet)
                    BatchNameMgr.ReturnObject(oUtility.theParams, "pr_SCM_SaveBatchFromOpnStock_Futures",
                                              ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveProgramList(DataTable dtProgramList, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, dtProgramList.Rows[i]["id"].ToString());
                    oUtility.AddParameters("@ProgramId", SqlDbType.VarChar, dtProgramList.Rows[i]["ProgramId"].ToString());
                    oUtility.AddParameters("@ProgramName", SqlDbType.VarChar,
                                             dtProgramList.Rows[i]["ProgramName"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtProgramList.Rows[i]["Status"].ToString());
                    oUtility.AddParameters("@FiscalYearMonth", SqlDbType.Int,
                                             dtProgramList.Rows[i]["FiscalYearMonth"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ProgramList.ReturnObject(oUtility.theParams, "pr_SCM_SaveProgramMaster_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveUpdateItemList(DataTable dtItemList, string CategoryID, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;

                foreach (DataRow theDR in dtItemList.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, theDR["id"].ToString());
                    oUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDR["Status"].ToString());
                    oUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    oUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                    oUtility.AddParameters("@TableName", SqlDbType.Int, TableName.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetSupplierList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject SupplierList = new ClsObject();
                return
                    (DataSet)
                    SupplierList.ReturnObject(oUtility.theParams, "pr_SCM_GetSupplierList_Futures",
                                              ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveSupplierList(DataTable dtSupplierList, int UserID)
        {
            lock (this)
            {
                ClsObject SupplierList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtSupplierList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, dtSupplierList.Rows[i]["id"].ToString());
                    oUtility.AddParameters("@SupplierId", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierId"].ToString());
                    oUtility.AddParameters("@SupplierName", SqlDbType.VarChar,
                                             dtSupplierList.Rows[i]["SupplierName"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtSupplierList.Rows[i]["Status"].ToString());
                    oUtility.AddParameters("@Address", SqlDbType.VarChar, dtSupplierList.Rows[i]["Address"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        SupplierList.ReturnObject(oUtility.theParams, "pr_SCM_SaveSupplierMaster_Futures",
                                                  ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataTable GetItemType()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(oUtility.theParams, "[pr_SCM_GetItemType_Futures]",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataTable GetSubItemType()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemType = new ClsObject();
                return
                    (DataTable)
                    ItemType.ReturnObject(oUtility.theParams, "[pr_SCM_GetSubItemType_Futures]",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }

        public DataSet GetDrugType(int itemTypeId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                ClsObject DrugList = new ClsObject();
                return
                    (DataSet)
                    DrugList.ReturnObject(oUtility.theParams, "[pr_SCM_GetDrugType_Futures]",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }
        
        public DataSet GetItemList(int itemTypeId, int Subtypeid, int programId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                oUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                oUtility.AddParameters("@programId", SqlDbType.Int, programId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetItemList_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemList(int Subtypeid)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                oUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetDrugList_Futures", ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListSupplier(int itemTypeId, int Subtypeid, int SupplierId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                oUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                oUtility.AddParameters("@SupplierId", SqlDbType.Int, SupplierId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetItemListSupplier_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListStore_Filtered(int itemTypeId, int Subtypeid, int StoreId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemTypeId.ToString());
                oUtility.AddParameters("@SubitemId", SqlDbType.Int, Subtypeid.ToString());
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetItemListStoreFiltered_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemListStore(int StoreId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemList = new ClsObject();
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetItemListStore_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetCommonItemList(String CategoryId, String TableName)
        {
            lock (this)
            {
                ClsObject ObjCommonItemList = new ClsObject();
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@CategoryId", SqlDbType.VarChar, CategoryId.ToString());
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                return
                    (DataTable)
                    ObjCommonItemList.ReturnObject(oUtility.theParams, "pr_SCM_GetCommonItemList_Futures",
                                                   ClsUtility.ObjectEnum.DataTable);
            }
        }

        public int SaveSubItemList(ArrayList dtSubitemList, int itemID, int UserID)
        {
            lock (this)
            {
                ClsObject subItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                theRowAffected =
                    (int)
                    subItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeleteItemdrugType_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i <= dtSubitemList.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@DrugTypeId", SqlDbType.Int, dtSubitemList[i].ToString());
                    oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemID.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        subItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveItemdrugType_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveItemList(DataTable dtItemList, int itematypeID, int UserID, int ProgramID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                //oUtility.Init_Hashtable();
                //oUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                //oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                //theRowAffected =
                //    (int)
                //    objItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeleteItemList_Futures",
                //                             ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i < dtItemList.Rows.Count; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    oUtility.AddParameters("@ProgramId", SqlDbType.Int, ProgramID.ToString());
                    oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    //  oUtility.AddParameters("@DrugGeneric", SqlDbType.Int, dtItemList.Rows[i]["DrugGeneric"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    oUtility.AddParameters("@Checked", SqlDbType.Int, dtItemList.Rows[i]["Checked"].ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveItemList_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return Rec;
            }
        }

        public int SaveSupplierItemList(DataTable dtItemList, int itematypeID, int UserID, int supplierID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeletesupplierItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    oUtility.AddParameters("@SupplierId", SqlDbType.Int, supplierID.ToString());
                    oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itematypeID.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveSupplierItemList_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveStoreItemList_Filtered(DataTable dtItemList, int UserID, int StoreID,int itemtypeID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    oUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    oUtility.AddParameters("@ItemTypeId", SqlDbType.Int, itemtypeID.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveStoreItemList(DataTable dtItemList, int UserID, int StoreID)
        {
            lock (this)
            {
                ClsObject objItemList = new ClsObject();
                int Rec = 0;
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                theRowAffected =
                    (int)
                    objItemList.ReturnObject(oUtility.theParams, "pr_SCM_DeleteStoreItemList_Futures",
                                             ClsUtility.ObjectEnum.ExecuteNonQuery);
                for (int i = 0; i <= dtItemList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@ItemId", SqlDbType.Int, dtItemList.Rows[i]["ItemID"].ToString());
                    oUtility.AddParameters("@storeId", SqlDbType.Int, StoreID.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        objItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveItemListStore_Futures",
                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetDonorList()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject DonorList = new ClsObject();
                return
                    (DataSet)
                    DonorList.ReturnObject(oUtility.theParams, "pr_SCM_GetDonorList_Futures",
                                           ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveDonorList(DataTable dtDonorList, int UserID)
        {
            lock (this)
            {
                ClsObject DonorList = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtDonorList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, dtDonorList.Rows[i]["id"].ToString());
                    oUtility.AddParameters("@DonorId", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorId"].ToString());
                    oUtility.AddParameters("@DonorName", SqlDbType.VarChar, dtDonorList.Rows[i]["DonorName"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, dtDonorList.Rows[i]["Status"].ToString());
                    oUtility.AddParameters("@Donorshortname", SqlDbType.VarChar,
                                             dtDonorList.Rows[i]["Donorshortname"].ToString());
                    oUtility.AddParameters("@Srno", SqlDbType.Int, dtDonorList.Rows[i]["Srno"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        DonorList.ReturnObject(oUtility.theParams, "pr_SCM_SaveDonorMaster_Futures",
                                               ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetProgramDonorLnk()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ProgramDonorLnk = new ClsObject();
                return
                    (DataSet)
                    ProgramDonorLnk.ReturnObject(oUtility.theParams, "pr_SCM_GetDonorProgramLinking_Futures",
                                                 ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemMasterListing()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject ItemMasterManager = new ClsObject();
                return
                    (DataSet)
                    ItemMasterManager.ReturnObject(oUtility.theParams, "Pr_SCM_GetItemListing_Futures",
                                                   ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveUpdateStore(DataTable dtItemList, string CategoryID, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                foreach (DataRow theDR in dtItemList.Rows)
                {
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@Id", SqlDbType.Int, theDR["id"].ToString());
                    oUtility.AddParameters("@StoreId", SqlDbType.VarChar, theDR["StoreId"].ToString());
                    oUtility.AddParameters("@Name", SqlDbType.VarChar, theDR["Name"].ToString());
                    oUtility.AddParameters("@DeleteFlag", SqlDbType.Int, theDR["Status"].ToString());
                    oUtility.AddParameters("@CentralStore", SqlDbType.Int, theDR["CentralStore"].ToString());
                    oUtility.AddParameters("@DispensingStore", SqlDbType.VarChar, theDR["DispensingStore"].ToString());
                    oUtility.AddParameters("@SRNo", SqlDbType.Int, theDR["SRNo"].ToString());
                    oUtility.AddParameters("@CategoryID", SqlDbType.Int, CategoryID.ToString());
                    oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    theRowAffected =
                        (int)
                        ItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public int SaveProgramDonorLnk(DataTable dtProgramDonorLnk, int UserID)
        {
            lock (this)
            {
                ClsObject ProgramDonorLnk = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                for (int i = 0; i <= dtProgramDonorLnk.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@DonorId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["DonorId"].ToString());
                    oUtility.AddParameters("@ProgramId", SqlDbType.Int, dtProgramDonorLnk.Rows[i]["ProgramId"].ToString());
                    oUtility.AddParameters("@FundingStartDate", SqlDbType.DateTime, dtProgramDonorLnk.Rows[i]["FundingStartDate"].ToString());
                    oUtility.AddParameters("@FundingEndDate", SqlDbType.DateTime, dtProgramDonorLnk.Rows[i]["FundingEndDate"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
                    if (Rec == 1)
                        oUtility.AddParameters("@Delete", SqlDbType.Int, "1");
                    theRowAffected = (int)ProgramDonorLnk.ReturnObject(oUtility.theParams, "pr_SCM_SaveProgramDonorlnk_Futures", ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        public DataSet GetStoreDetail()
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject StoreMasterManager = new ClsObject();
                return
                    (DataSet)
                    StoreMasterManager.ReturnObject(oUtility.theParams, "Pr_SCM_GetStoreDetails_Futures",
                                                    ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataSet GetItemDetails(int theItemId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@ItemId", SqlDbType.Int, theItemId.ToString());
                ClsObject ItemManager = new ClsObject();
                return
                    (DataSet)
                    ItemManager.ReturnObject(oUtility.theParams, "pr_SCM_GetItemDetails_Futures",
                                             ClsUtility.ObjectEnum.DataSet);
            }
        }

        public int SaveUpdateStoreLinking(DataTable dtStoreList, string TableName, int UserID)
        {
            lock (this)
            {
                ClsObject ItemList = new ClsObject();
                int theRowAffected = 0;
                oUtility.Init_Hashtable();
                StringBuilder theSB = new StringBuilder();
                theSB.Append("Delete from " + TableName + " ");
                foreach (DataRow theDR in dtStoreList.Rows)
                {
                    theSB.Append("Insert into " + TableName +
                                 "(SourceStore, DestinationStore, UserId, CreateDate, UpdateDate) ");
                    theSB.Append("values (" + theDR["SourceStore"].ToString() + "," + theDR["DestinationStore"].ToString() +
                                 "," + UserID + ", getdate(), getdate())");
                }
                oUtility.AddParameters("@Str", SqlDbType.VarChar, theSB.ToString());
                oUtility.AddParameters("@TableName", SqlDbType.VarChar, TableName.ToString());
                theRowAffected =
                    (int)
                    ItemList.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateItemMasterList_Futures",
                                          ClsUtility.ObjectEnum.ExecuteNonQuery);

                return theRowAffected;
            }
        }

        public int SaveUpdateItemMaster(Hashtable theHash)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                oUtility.AddParameters("@Drug_Pk", SqlDbType.Int, theHash["Drug_Pk"].ToString());
                oUtility.AddParameters("@ItemCode", SqlDbType.VarChar, theHash["ItemCode"].ToString());
                oUtility.AddParameters("@FDACode", SqlDbType.VarChar, theHash["FDACode"].ToString());
                oUtility.AddParameters("@DispensingUnit", SqlDbType.Int, theHash["DispensingUnit"].ToString());
                oUtility.AddParameters("@PurchaseUnit", SqlDbType.Int, theHash["PurchaseUnit"].ToString());
                oUtility.AddParameters("@PurchaseUnitQty", SqlDbType.Int, theHash["PurchaseUnitQty"].ToString());
                oUtility.AddParameters("@PurchaseUnitPrice", SqlDbType.Decimal, theHash["PurchaseUnitPrice"].ToString());
                oUtility.AddParameters("@Manufacturer", SqlDbType.Int, theHash["Manufacturer"].ToString());
                oUtility.AddParameters("@DispensingUnitPrice", SqlDbType.Decimal, theHash["DispensingUnitPrice"].ToString());
                oUtility.AddParameters("@DispensingMargin", SqlDbType.Decimal, theHash["DispensingMargin"].ToString());
                oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal, theHash["SellingPrice"].ToString());
                oUtility.AddParameters("@EffectiveDate", SqlDbType.DateTime, theHash["EffectiveDate"].ToString());
                oUtility.AddParameters("@Status", SqlDbType.Int, theHash["Status"].ToString());
                oUtility.AddParameters("@MinStock", SqlDbType.Int, theHash["MinQty"].ToString());
                oUtility.AddParameters("@MaxStock", SqlDbType.Int, theHash["MaxQty"].ToString());
                oUtility.AddParameters("@UserId", SqlDbType.Int, theHash["UserId"].ToString());
                oUtility.AddParameters("@ItemInstructions", SqlDbType.Int, theHash["ItemInstructions"].ToString());

                oUtility.AddParameters("@DispenseUnitQty", SqlDbType.Int, theHash["DispenseUnitQty"].ToString());
                oUtility.AddParameters("@VolumeUnit", SqlDbType.Int, theHash["VolumeUnit"].ToString());
                oUtility.AddParameters("@MedicationAmt", SqlDbType.Int, theHash["MedicationAmt"].ToString());
                oUtility.AddParameters("@PerlblVolUnits", SqlDbType.Int, theHash["PerlblVolUnits"].ToString());
                oUtility.AddParameters("@DispesingUnit", SqlDbType.Int, theHash["DispesingUnit"].ToString());
                oUtility.AddParameters("@syrup", SqlDbType.Int, theHash["Syrup"].ToString());


                ClsObject theItemManager = new ClsObject();
                return
                    (Int32)
                    theItemManager.ReturnObject(oUtility.theParams, "pr_SCM_SaveUpdateItemMaster_Futures",
                                                ClsUtility.ObjectEnum.ExecuteNonQuery);
            }
        }

        public DataSet GetStoreUserLink(int StoreId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
                return
                    (DataSet)
                    UserList.ReturnObject(oUtility.theParams, "pr_SCM_GetStoreUserLinking_Futures",
                                          ClsUtility.ObjectEnum.DataSet);
            }
        }

        public DataTable GetStoreByUser(int UserId)
        {
            lock (this)
            {
                oUtility.Init_Hashtable();
                ClsObject UserList = new ClsObject();
                oUtility.AddParameters("@UserId", SqlDbType.Int, UserId.ToString());
                return
                    (DataTable)
                    UserList.ReturnObject(oUtility.theParams, "Pr_SCM_GetStoreNameByUserID_Futures",
                                          ClsUtility.ObjectEnum.DataTable);
            }
        }

        public int SaveUpdateStoreUserLinking(DataTable dtStoreUserList)
        {
            lock (this)
            {
                ClsObject StoreUserLnk = new ClsObject();
                int Rec = 0;

                int theRowAffected = 0;

                oUtility.Init_Hashtable();
                oUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[0]["StoreID"].ToString());
                theRowAffected =
                    (int)
                    StoreUserLnk.ReturnObject(oUtility.theParams, "pr_SCM_DeleteStoreUserlnk_Futures",
                                              ClsUtility.ObjectEnum.ExecuteNonQuery);

                for (int i = 0; i <= dtStoreUserList.Rows.Count - 1; i++)
                {
                    Rec = Rec + 1;
                    oUtility.Init_Hashtable();
                    oUtility.AddParameters("@StoreId", SqlDbType.Int, dtStoreUserList.Rows[i]["StoreID"].ToString());
                    oUtility.AddParameters("@UserId", SqlDbType.Int, dtStoreUserList.Rows[i]["USerId"].ToString());
                    theRowAffected =
                        (int)
                        StoreUserLnk.ReturnObject(oUtility.theParams, "pr_SCM_SaveStoreUserlnk_Futures",
                                                  ClsUtility.ObjectEnum.ExecuteNonQuery);
                }
                return theRowAffected;
            }
        }

        //public DataSet GetPurcaseOrderItem(int isPO, int UserID, int StoreID)
        //{
        //    oUtility.Init_Hashtable();
        //    ClsObject GetPurcahseItem = new ClsObject();
        //    oUtility.AddParameters("@isPO", SqlDbType.Int, isPO.ToString());
        //    oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //    oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreID.ToString());

        //    return
        //        (DataSet)
        //        GetPurcahseItem.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurcaseOrderItem",
        //                                     ClsUtility.ObjectEnum.DataSet);
        //}

        //public DataSet GetStockforAdjustment(int StoreId, string AdjustmentDate)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject GetStockItem = new ClsObject();
        //        oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
        //        return
        //            (DataSet)
        //            GetStockItem.ReturnObject(oUtility.theParams, "Pr_SCM_GetStockforAdjustment_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public int SavePurchaseOrder(DataTable DtMasterPO, DataTable dtPOItems, bool isUpdate)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PODetail = new ClsObject();
        //        PODetail.Connection = this.Connection;
        //        PODetail.Transaction = this.Transaction;
        //        int theRowAffected = 0;
        //        int POID = 0;
        //        DataRow theDR;

        //        oUtility.Init_Hashtable();

        //        oUtility.AddParameters("@LocationID", SqlDbType.VarChar, DtMasterPO.Rows[0]["LocationID"].ToString());
        //        oUtility.AddParameters("@SupplierID", SqlDbType.Int, DtMasterPO.Rows[0]["SupplierID"].ToString());
        //        oUtility.AddParameters("@OrderDate", SqlDbType.Int, DtMasterPO.Rows[0]["OrderDate"].ToString());
        //        oUtility.AddParameters("@PreparedBy", SqlDbType.VarChar, DtMasterPO.Rows[0]["PreparedBy"].ToString());
        //        oUtility.AddParameters("@SourceStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["SrcStore"].ToString());
        //        oUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DtMasterPO.Rows[0]["DestStore"].ToString());
        //        oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());
        //        oUtility.AddParameters("@AuthorizedBy", SqlDbType.Int, DtMasterPO.Rows[0]["AthorizedBy"].ToString());
        //        if (isUpdate)
        //        {
        //            oUtility.AddParameters("@Poid", SqlDbType.Int, DtMasterPO.Rows[0]["POID"].ToString());
        //            oUtility.AddParameters("@IsUpdate", SqlDbType.Bit, isUpdate.ToString());

        //            if (Convert.ToString(DtMasterPO.Rows[0]["IsRejectedStatus"]) == "1")
        //            {
        //                oUtility.AddParameters("@Status", SqlDbType.Int, "5");
        //            }
        //            else
        //            {
        //                if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
        //                {
        //                    oUtility.AddParameters("@Status", SqlDbType.Int, "1");
        //                }
        //                else
        //                {
        //                    oUtility.AddParameters("@Status", SqlDbType.Int, "2");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (Convert.ToString(DtMasterPO.Rows[0]["AthorizedBy"]) == "0")
        //            {
        //                oUtility.AddParameters("@Status", SqlDbType.Int, "1");
        //            }
        //            else
        //            {
        //                oUtility.AddParameters("@Status", SqlDbType.Int, "2");
        //            }
        //        }

        //        theDR =
        //            (DataRow)
        //            PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderMaster_Futures",
        //                                  ClsUtility.ObjectEnum.DataRow);
        //        POID = System.Convert.ToInt32(theDR[0].ToString());

        //        if (isUpdate)
        //        {
        //            oUtility.Init_Hashtable();
        //            oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
        //            theRowAffected =
        //                (int)
        //                PODetail.ReturnObject(oUtility.theParams, "pr_SCM_DeletePurchaseOrderItem_Futures",
        //                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        for (int i = 0; i < dtPOItems.Rows.Count; i++)
        //        {
        //            oUtility.Init_Hashtable();
        //            oUtility.AddParameters("@POId", SqlDbType.Int, POID.ToString());
        //            oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtPOItems.Rows[i]["ItemId"].ToString());
        //            oUtility.AddParameters("@Quantity", SqlDbType.Int, dtPOItems.Rows[i]["Quantity"].ToString());
        //            oUtility.AddParameters("@PurchasePrice", SqlDbType.Decimal,
        //                                     dtPOItems.Rows[i]["priceperunit"].ToString());
        //            //  oUtility.AddParameters("@Unit", SqlDbType.Int,dtPOItems.Rows[i]["Units"].ToString());
        //            oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterPO.Rows[0]["UserID"].ToString());

        //            oUtility.AddParameters("@BatchID", SqlDbType.Int, dtPOItems.Rows[i]["BatchID"].ToString());
        //            oUtility.AddParameters("@AvaliableQty", SqlDbType.Int, dtPOItems.Rows[i]["AvaliableQty"].ToString());
        //            oUtility.AddParameters("@ExpiryDate", SqlDbType.Int, dtPOItems.Rows[i]["ExpiryDate"].ToString());

        //            theRowAffected =
        //                (int)
        //                PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SavePurchaseOrderItem_Futures",
        //                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return POID;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);

        //    }
        //}


        //public DataSet GetOpenStock()
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject OpeningStock = new ClsObject();
        //        return
        //            (DataSet)
        //            OpeningStock.ReturnObject(oUtility.theParams, "pr_SCM_GetOpeningStock_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataTable GetPurchaseOrderDetails(Int32 UserID, Int32 DestinStoreID, Int32 locationID)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //        oUtility.AddParameters("@DestinStoreID", SqlDbType.Int, DestinStoreID.ToString());
        //        oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurchaseDetails_Futures",
        //                                      ClsUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataTable GetPurchaseOrderDetailsForGRN(Int32 UserID, Int32 StoreID, Int32 locationID)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@UserID", SqlDbType.Int, UserID.ToString());
        //        oUtility.AddParameters("@StoreID", SqlDbType.Int, StoreID.ToString());
        //        oUtility.AddParameters("@LocationID", SqlDbType.Int, locationID.ToString());

        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetPurchaseDetailsForGRN_Futures",
        //                                      ClsUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public int SaveUpdateOpeningStock(DataTable theDTOPStock, Int32 UserID, DateTime TransactionDate)
        //{
        //    ClsObject StoreUserLnk = new ClsObject();
        //    int theRowAffected = 0;
        //    for (int i = 0; i < theDTOPStock.Rows.Count; i++)
        //    {
        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTOPStock.Rows[i]["ItemId"].ToString());
        //        oUtility.AddParameters("@BatchId", SqlDbType.Int, theDTOPStock.Rows[i]["BatchId"].ToString());
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, theDTOPStock.Rows[i]["StoreId"].ToString());
        //        oUtility.AddParameters("@Quantity", SqlDbType.Int, theDTOPStock.Rows[i]["Quantity"].ToString());
        //        oUtility.AddParameters("@ExpiryDate ", SqlDbType.VarChar,
        //                                 theDTOPStock.Rows[i]["ExpiryDate"].ToString());
        //        oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
        //        oUtility.AddParameters("@TransactionDate", SqlDbType.DateTime, TransactionDate.ToString());
        //        theRowAffected =
        //            (int)
        //            StoreUserLnk.ReturnObject(oUtility.theParams, "pr_SCM_SaveOpeningStock_Futures",
        //                                      ClsUtility.ObjectEnum.ExecuteNonQuery);
        //    }
        //    return theRowAffected;
        //}

        //public DataSet GetPurchaseOrderDetailsByPoid(Int32 POId)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetPurchaseOrderDetailsByPoid_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public int SaveUpdateStockAdjustment(DataTable theDTAdjustStock, int LocationId, int StoreId,
        //                                     string AdjustmentDate, int AdjustmentPreparedBy, int AdjustmentAuthorisedBy,
        //                                     int Updatestock, int UserID)
        //{
        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject ObjStoreAdjust = new ClsObject();
        //        int theRowAffected = 0;
        //        oUtility.Init_Hashtable();

        //        oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@AdjustmentDate", SqlDbType.VarChar, AdjustmentDate.ToString());
        //        oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
        //        oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
        //        DataRow theDR = (DataRow)ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures", ClsUtility.ObjectEnum.DataRow);

        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        theRowAffected = (int) ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_DeleteStockforAdjustment_Futures",ClsUtility.ObjectEnum.ExecuteNonQuery);
     
        //        for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
        //        {
        //            if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
        //            {
        //                if (Updatestock == 1)
        //                {
        //                    oUtility.Init_Hashtable();
        //                    oUtility.AddParameters("@Updatestock", SqlDbType.Int, Updatestock.ToString());
        //                    oUtility.AddParameters("@ItemId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["ItemId"].ToString());
        //                    oUtility.AddParameters("@BatchId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["BatchId"].ToString());
        //                    oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                             theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
        //                    oUtility.AddParameters("@StoreId", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["StoreId"].ToString());
        //                    oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
        //                                             theDTAdjustStock.Rows[i]["AdjQty"].ToString());
        //                    oUtility.AddParameters("@UserId", SqlDbType.Int, UserID.ToString());
        //                    theRowAffected =
        //                        (int)
        //                        ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockTransAdjust_Futures",
        //                                                    ClsUtility.ObjectEnum.ExecuteNonQuery);
        //                }
        //            }
        //        }

        //        for (int i = 0; i < theDTAdjustStock.Rows.Count; i++)
        //        {
        //            if (Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) > 0 || Convert.ToInt32(theDTAdjustStock.Rows[i]["AdjQty"]) < 0)
        //            {
        //                oUtility.Init_Hashtable();
        //                oUtility.AddParameters("@UpdateStock", SqlDbType.Int, Updatestock.ToString());
        //                oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //                oUtility.AddParameters("@AdjustmentPreparedBy", SqlDbType.Int, AdjustmentPreparedBy.ToString());
        //                oUtility.AddParameters("@AdjustmentAuthorisedBy", SqlDbType.Int, AdjustmentAuthorisedBy.ToString());
        //                oUtility.AddParameters("@AdjustmentDate", SqlDbType.DateTime, AdjustmentDate.ToString());
        //                oUtility.AddParameters("@AdjustmentId", SqlDbType.Int, theDR["AdjustId"].ToString());
        //                oUtility.AddParameters("@ItemId", SqlDbType.Int, theDTAdjustStock.Rows[i]["ItemId"].ToString());
        //                oUtility.AddParameters("@BatchId", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["BatchId"].ToString());
        //                oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                         theDTAdjustStock.Rows[i]["ExpiryDate"].ToString());
        //                oUtility.AddParameters("@StoreId", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["StoreId"].ToString());
        //                oUtility.AddParameters("@PurchaseUnit", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["UnitId"].ToString());
        //                oUtility.AddParameters("@AdjustReasonId ", SqlDbType.VarChar,
        //                                         theDTAdjustStock.Rows[i]["AdjustReasonId"].ToString());
        //                oUtility.AddParameters("@AdjustmentQuantity", SqlDbType.Int,
        //                                         theDTAdjustStock.Rows[i]["AdjQty"].ToString());
        //                oUtility.AddParameters("@UserId ", SqlDbType.Int, UserID.ToString());
        //                theRowAffected =
        //                    (int)
        //                    ObjStoreAdjust.ReturnObject(oUtility.theParams, "Pr_SCM_SaveStockOrdAdjust_Futures",
        //                                                ClsUtility.ObjectEnum.ExecuteNonQuery);
        //            }
        //        }
        //        return theRowAffected;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataSet GetPurchaseOrderDetailsByPoidGRN(Int32 POId)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@Poid", SqlDbType.Int, POId.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetPurchaseOrderGRNByPoid_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public int SaveGoodreceivedNotes(DataTable DtMasterGRN, DataTable dtGRNItems, int IsPOorIST)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);
        //        ClsObject PODetail = new ClsObject();
        //        PODetail.Connection = this.Connection;
        //        PODetail.Transaction = this.Transaction;
        //        int theRowAffected = 0;
        //        int GrnId = 0;
        //        DataRow theDR;

        //        if (DtMasterGRN.Rows.Count > 0)
        //        {
        //            if (!String.IsNullOrEmpty(Convert.ToString(DtMasterGRN.Rows[0]["GRNId"])))
        //            {
        //                if (Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]) == 0)
        //                {
        //                    oUtility.Init_Hashtable();
        //                    oUtility.AddParameters("@POId", SqlDbType.VarChar, DtMasterGRN.Rows[0]["POId"].ToString());
        //                    oUtility.AddParameters("@LocationID", SqlDbType.Int,
        //                                             DtMasterGRN.Rows[0]["LocationID"].ToString());
        //                    oUtility.AddParameters("@RecievedStoreID", SqlDbType.Int,
        //                                             DtMasterGRN.Rows[0]["DestinStoreID"].ToString());
        //                    oUtility.AddParameters("@Freight", SqlDbType.VarChar,
        //                                             DtMasterGRN.Rows[0]["Freight"].ToString());
        //                    oUtility.AddParameters("@Tax", SqlDbType.Int, DtMasterGRN.Rows[0]["Tax"].ToString());
        //                    oUtility.AddParameters("@UserID", SqlDbType.Int, DtMasterGRN.Rows[0]["UserID"].ToString());
        //                    theDR =
        //                        (DataRow)
        //                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNMaster_Futures",
        //                                              ClsUtility.ObjectEnum.DataRow);
        //                    GrnId = System.Convert.ToInt32(theDR[0].ToString());
        //                }
        //            }
        //        }

        //        if (GrnId == 0)
        //        {
        //            GrnId = Convert.ToInt32(DtMasterGRN.Rows[0]["GRNId"]);
        //        }
        //        for (int i = 0; i < dtGRNItems.Rows.Count; i++)
        //        {
        //            oUtility.Init_Hashtable();
        //            if (!String.IsNullOrEmpty(Convert.ToString(dtGRNItems.Rows[i]["GRNId"].ToString())))
        //            {
        //                if (Convert.ToInt32(dtGRNItems.Rows[i]["GRNId"].ToString()) == 0)
        //                {

        //                    oUtility.AddParameters("@GRNId", SqlDbType.Int, GrnId.ToString());
        //                    oUtility.AddParameters("@ItemId", SqlDbType.VarChar, dtGRNItems.Rows[i]["ItemId"].ToString());
        //                    //oUtility.AddParameters("@BatchID", SqlDbType.Int, dtGRNItems.Rows[i]["BatchID"].ToString());
        //                    oUtility.AddParameters("@batchName", SqlDbType.VarChar,
        //                                             dtGRNItems.Rows[i]["batchName"].ToString());
        //                    oUtility.AddParameters("@RecievedQuantity", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["RecievedQuantity"].ToString());

        //                    oUtility.AddParameters("@FreeRecievedQuantity", SqlDbType.Int,
        //                        (Convert.ToString(dtGRNItems.Rows[i]["FreeRecievedQuantity"]) == "") ? "0" : dtGRNItems.Rows[i]["FreeRecievedQuantity"].ToString());

        //                    oUtility.AddParameters("@PurchasePrice", SqlDbType.Int,
        //                                        (Convert.ToString(dtGRNItems.Rows[i]["ItemPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["ItemPurchasePrice"].ToString());
        //                    oUtility.AddParameters("@TotPurchasePrice", SqlDbType.Int,
        //                                            (Convert.ToString(dtGRNItems.Rows[i]["TotPurchasePrice"]) == "") ? "0" : dtGRNItems.Rows[i]["TotPurchasePrice"].ToString());
        //                    oUtility.AddParameters("@SellingPrice", SqlDbType.Decimal,
        //                                            (Convert.ToString(dtGRNItems.Rows[i]["SellingPrice"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPrice"].ToString());
        //                    oUtility.AddParameters("@SellingPricePerDispense", SqlDbType.Decimal,
        //                                             (Convert.ToString(dtGRNItems.Rows[i]["SellingPricePerDispense"]) == "") ? "0" : dtGRNItems.Rows[i]["SellingPricePerDispense"].ToString());
        //                    oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                             dtGRNItems.Rows[i]["ExpiryDate"].ToString());

        //                    oUtility.AddParameters("@UserID", SqlDbType.Int,
        //                         (Convert.ToString(dtGRNItems.Rows[i]["UserID"]) == "") ? DtMasterGRN.Rows[0]["UserID"].ToString() : dtGRNItems.Rows[i]["UserID"].ToString());
        //                    oUtility.AddParameters("@IsPOorIST", SqlDbType.Int, IsPOorIST.ToString());
        //                    oUtility.AddParameters("@POId", SqlDbType.VarChar, dtGRNItems.Rows[i]["POId"].ToString());
        //                    oUtility.AddParameters("@Margin", SqlDbType.Decimal, dtGRNItems.Rows[i]["Margin"].ToString());
        //                    oUtility.AddParameters("@destinationStoreID", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["DestinStoreID"].ToString());
        //                    oUtility.AddParameters("@SourceStoreID", SqlDbType.Int,
        //                                             dtGRNItems.Rows[i]["SourceStoreID"].ToString());


        //                    theRowAffected =
        //                        (int)
        //                        PODetail.ReturnObject(oUtility.theParams, "pr_SCM_SaveGRNItems_Futures",
        //                                              ClsUtility.ObjectEnum.ExecuteNonQuery);
        //                }
        //            }
        //        }

        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return GrnId;
        //    }
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);

        //    }
        //}


        //public DataTable GetExperyReport(int StoreId, DateTime TransDate, DateTime ExpiryDate)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@Storeid", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@TransDate", SqlDbType.Date, TransDate.ToString());
        //        oUtility.AddParameters("@ExpiryDate", SqlDbType.Date, ExpiryDate.ToString());
        //        return
        //            (DataTable)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetExperyReport_Futures",
        //                                      ClsUtility.ObjectEnum.DataTable);
        //    }
        //    catch
        //    {
        //        //DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public DataSet GetStockSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
        //        oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
        //        oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetStockSummary_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}


        //public DataSet GetDisposeStock(int StoreId, DateTime AsofDate)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@AsofDate", SqlDbType.DateTime, AsofDate.ToString());

        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetDisposeStock_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public int SaveDisposeItems(int StoreId, int LocationId, DateTime AsofDate, int UserId, DataTable theDT)
        //{

        //    try
        //    {
        //        this.Connection = DataMgr.GetConnection();
        //        this.Transaction = DataMgr.BeginTransaction(this.Connection);

        //        ClsObject ObjStoreDispose = new ClsObject();
        //        int theRowAffected = 0;
        //        oUtility.Init_Hashtable();
        //        oUtility.AddParameters("@LocationId", SqlDbType.Int, LocationId.ToString());
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@DisposeDate", SqlDbType.VarChar, AsofDate.ToString());
        //        oUtility.AddParameters("@DisposePreparedBy", SqlDbType.Int, UserId.ToString());
        //        oUtility.AddParameters("@DisposeAuthorisedBy", SqlDbType.Int, UserId.ToString());
        //        DataRow theDR = (DataRow)ObjStoreDispose.ReturnObject(oUtility.theParams, "pr_SCM_SaveDisposeItems_Futures", ClsUtility.ObjectEnum.DataRow);
        //        for (int i = 0; i < theDT.Rows.Count; i++)
        //        {
        //            oUtility.Init_Hashtable();
        //            if (Convert.ToInt32(! DBNull.Value.Equals(theDT.Rows[i]["Dispose"])) == 1)
        //            {
        //                oUtility.AddParameters("@DisposeId", SqlDbType.Int, theDR["DisposeId"].ToString());
        //                oUtility.AddParameters("@ItemId", SqlDbType.Int, theDT.Rows[i]["ItemId"].ToString());
        //                oUtility.AddParameters("@BatchId", SqlDbType.Int, theDT.Rows[i]["BatchId"].ToString());
        //                oUtility.AddParameters("@ExpiryDate", SqlDbType.DateTime,
        //                                         theDT.Rows[i]["ExpiryDate"].ToString());
        //                oUtility.AddParameters("@StoreId", SqlDbType.Int, theDT.Rows[i]["StoreId"].ToString());
        //                oUtility.AddParameters("@Quantity", SqlDbType.Int, "-" + theDT.Rows[i]["Quantity"].ToString());
        //                oUtility.AddParameters("@UserId ", SqlDbType.Int, UserId.ToString());
        //                theRowAffected =
        //                    (int)
        //                    ObjStoreDispose.ReturnObject(oUtility.theParams, "pr_SCM_SaveDisposeItems_Futures",
        //                                                 ClsUtility.ObjectEnum.ExecuteNonQuery);
        //            }
        //        }
        //        DataMgr.CommitTransaction(this.Transaction);
        //        DataMgr.ReleaseConnection(this.Connection);
        //        return theRowAffected;
        //    }
           
            
        //    catch
        //    {
        //        DataMgr.RollBackTransation(this.Transaction);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }

        //}

        //public DataSet GetBatchSummary(int StoreId, int ItemId, DateTime FromDate, DateTime ToDate)
        //{
        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@ItemId", SqlDbType.Int, ItemId.ToString());
        //        oUtility.AddParameters("@FromDate", SqlDbType.DateTime, FromDate.ToString());
        //        oUtility.AddParameters("@ToDate", SqlDbType.DateTime, ToDate.ToString());
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "pr_SCM_GetBatchSummary_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }
        //}

        //public DataSet GetStockLedgerData(int StoreId, DateTime FromDate, DateTime ToDate)
        //{

        //    try
        //    {
        //        oUtility.Init_Hashtable();
        //        ClsObject objPOdetails = new ClsObject();
        //        oUtility.AddParameters("@StoreId", SqlDbType.Int, StoreId.ToString());
        //        oUtility.AddParameters("@StartDate", SqlDbType.DateTime, FromDate.ToString());
        //        oUtility.AddParameters("@EndDate", SqlDbType.DateTime, ToDate.ToString());
        //        oUtility.AddParameters("@Password", SqlDbType.VarChar, ApplicationAccess.DBSecurity);
        //        return
        //            (DataSet)
        //            objPOdetails.ReturnObject(oUtility.theParams, "Pr_SCM_GetStockLedger_Futures",
        //                                      ClsUtility.ObjectEnum.DataSet);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        if (this.Connection != null)
        //            DataMgr.ReleaseConnection(this.Connection);
        //    }

        //}


    }
}


