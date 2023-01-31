using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_Management
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Properties.Settings.Default.USERNAME;
        }
        
        private void btnManageCustomer_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Customer", "User_Customer");
            if (check)
            {
                Frm_Customer frm = new Frm_Customer();
                frm.ShowDialog();
            }
                

            // difference between show and showDialog
            // with show i can open multiple forms at the same time
            // with ShowDialog i can not open multiple forms at the same time
        }

        private void btnManageSupplier_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Supp_Data", "User_Supplier");
            if (check)
            {
                Frm_Suppliers frm = new Frm_Suppliers();
                frm.ShowDialog();
            }
                
        }

        private void btnDeservedType_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Des_Type", "User_Deserved");
            if (check)
            {
                Frm_DeservedType frm = new Frm_DeservedType();
                frm.ShowDialog();
            }
                
        }

        private void btnManageDeserved_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Deserved", "User_Deserved");
            if (check)
            {
                Frm_Deserved frm = new Frm_Deserved();
                frm.ShowDialog();
            }
                
        }

        private void btnDeservedReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("DeservedReport", "User_Deserved");
            if (check)
            {
                Frm_DeservedReport frm = new Frm_DeservedReport();
                frm.ShowDialog();
            }
                
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Item_Add", "User_Setting");
            if (check)
            {
                Frm_Products frm = new Frm_Products();
                frm.ShowDialog();
            }
                
        }

        private void btnManageBuys_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Buy", "User_Buy");
            if (check)
            {
                Frm_Buy frm = new Frm_Buy();
                frm.ShowDialog();
            }
                
        }
        Database db = new Database();
        private void btnTakeBackup_Click(object sender, EventArgs e)
        {
            bool check = checkUser("TakeBackUp", "User_BackUp");
            if (check)
            {
                try
                {
                    string d = DateTime.Now.Date.ToString("dd-MM-yyyy");
                    SaveFileDialog open = new SaveFileDialog();

                    open.Filter = "BackUp Files (*.Back) | *.back";
                    open.FileName = "Sales_BackUP_" + d + "";

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        db.executeData("backup database Sales_System To Disk='" + open.FileName + "'", "تم اخذ نسخه احتياطية بنجاح", "");
                    }
                }
                catch (Exception)
                {

                }
            }
                
        }
        //Database db = new Database();
        private void btnRestoreBackup_Click(object sender, EventArgs e)
        {
            bool check = checkUser("ReturnBackUp", "User_BackUp");
            if (check)
            {
                //Server server = new Server(@".\SQLEXPRESS");
                //Microsoft.SqlServer.Management.Smo.Database db = server.Databases["Sales_System"];
                //if (db != null)
                //{
                // server.KillAllProcesses(db.Name);
                //}
                //Restore restore = new Restore();
                //restore.Database = db.Name;
                //restore.Action = RestoreActionType.Database;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "BackUp Files (*.Back) | *.back";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    //restore.Devices.AddDevice(open.FileName, DeviceType.File);
                    //restore.ReplaceDatabase = true;
                    //restore.NoRecovery = false;
                    //restore.SqlRestore(server);
                    //MessageBox.Show("تم استرجاع السنخه الاحتياطية بنجاح", "تاكيد");
                    SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Sales_System;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand();

                    string database = conn.Database.ToString();
                    //MessageBox.Show(database);
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    try
                    {
                        string str1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                        SqlCommand cmd1 = new SqlCommand(str1, conn);
                        cmd1.ExecuteNonQuery();

                        string str2 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + open.FileName + "' WITH REPLACE;";
                        SqlCommand cmd2 = new SqlCommand(str2, conn);
                        cmd2.ExecuteNonQuery();

                        string str3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                        SqlCommand cmd3 = new SqlCommand(str3, conn);
                        cmd3.ExecuteNonQuery();


                        MessageBox.Show("تم استرجاع النسخة الاحتياطية بنجاح");

                        conn.Close();

                    }
                    catch { }

                }
            }
                
        }

        private void btnSupplierMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Supp_Money", "User_Supplier");
            if (check)
            {
                Frm_SupplierMoney frm = new Frm_SupplierMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnSupplierReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Supp_Report", "User_Supplier");
            if (check)
            {
                Frm_SupplierReport frm = new Frm_SupplierReport();
                frm.ShowDialog();
            }
                
        }

        private void btnBuyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("BuyReport", "User_Buy");
            if (check)
            {
                Frm_BuyReport frm = new Frm_BuyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnManageSales_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Sale", "User_Sale");
            if (check)
            {
                Frm_Sales frm = new Frm_Sales();
                frm.ShowDialog();
            }
                
        }

        private void btnClientsMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("CustomerMoney", "User_Customer");
            if (check)
            {
                Frm_CustomerMoney frm = new Frm_CustomerMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnCustomerReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("CustomerReport", "User_Customer");
            if (check)
            {
                Frm_CustomerReport frm = new Frm_CustomerReport();
                frm.ShowDialog();
            }
                
        }

        private void btnSalesReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("SaleReport", "User_Sale");
            if (check)
            {
                Frm_SalesReport frm = new Frm_SalesReport();
                frm.ShowDialog();
            }
                
        }

        private void btnSalesRb7h_Click(object sender, EventArgs e)
        {
            bool check = checkUser("SaleRb7h", "User_Sale");
            if (check)
            {
                Frm_SalesRb7h frm = new Frm_SalesRb7h();
                frm.ShowDialog();
            }
                
        }

        private void btnReportMortagaa_Click(object sender, EventArgs e)
        {
            bool check = checkUser("ReturnReport", "User_Return");
            if (check)
            {
                Frm_ReturnReport frm = new Frm_ReturnReport();
                frm.ShowDialog();

            }
                
        }

        private void btnManageMortagaa_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Return_", "User_Return");
            if (check)
            {
                Frm_Return frm = new Frm_Return();
                frm.ShowDialog();
            }
               
        }

        private void btnNewEmp_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Add_Emp", "User_Emp");
            if (check)
            {
                Frm_Employee frm = new Frm_Employee();
                frm.ShowDialog();
            }
               
        }

        
        private void BtnEmployeeBorrowItems_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Borrow_Items", "User_Emp");
            if (check)
            {
                Frm_EmployeeBorrowItems frm = new Frm_EmployeeBorrowItems();
                frm.ShowDialog();
            }
                
        }

        private void BtnEmployeeBorrowMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Borrow_Money", "User_Emp");
            if (check)
            {
                Frm_EmployeeBorrowMoney frm = new Frm_EmployeeBorrowMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Add_Stock", "User_StockBank");
            if (check)
            {
                Frm_AddStock frm = new Frm_AddStock();
                frm.ShowDialog();
            }
                
        }

        private void btnStockAddMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Stock_Insert", "User_StockBank");
            if (check)
            {
                Frm_StockAddMoney frm = new Frm_StockAddMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnBankMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Bank_Insert", "User_StockBank");
            if (check)
            {
                Frm_BankAddMoney frm = new Frm_BankAddMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnStockPullMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Stock_Pull", "User_StockBank");
            if (check)
            {
                Frm_StockPullMoney frm = new Frm_StockPullMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnBankPullMoney_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Bank_Pull", "User_StockBank");
            if (check)
            {
                Frm_BankPullMoney frm = new Frm_BankPullMoney();
                frm.ShowDialog();
            }
                
        }

        private void btnStockTransfire_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Transfire_Money", "User_StockBank");
            if (check)
            {
                Frm_StockTransfire frm = new Frm_StockTransfire();
                frm.ShowDialog();
            }
               
        }

        private void btnStockBankTransfire_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Transfire_BankStock", "User_StockBank");
            if (check)
            {
                Frm_StockBankTransfire frm = new Frm_StockBankTransfire();
                frm.ShowDialog();
            }
                
        }

        private void btnCurrentMoeny_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Current_Money", "User_StockBank");
            if (check)
            {
                Frm_CurrentMoeny frm = new Frm_CurrentMoeny();
                frm.ShowDialog();
            }
                
        }

        private void btnStockAddedMoneyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_StockAddedMoneyReport frm = new Frm_StockAddedMoneyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnBankAddedMoneyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_BankAddedMoneyReport frm = new Frm_BankAddedMoneyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnStockPullMoneyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_StockPullMoneyReport frm = new Frm_StockPullMoneyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnBankPullMoneyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_BankPullMoneyReport frm = new Frm_BankPullMoneyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnStockTransfireReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_StockTransfireReport frm = new Frm_StockTransfireReport();
                frm.ShowDialog();
            }
                
        }

        private void btnStockBankTransfireReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("StockBank_Report", "User_StockBank");
            if (check)
            {
                Frm_StockBankTransfireReport frm = new Frm_StockBankTransfireReport();
                frm.ShowDialog();
            }
                
        }

        private void btnEmpSalary_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Salary", "User_Emp");
            if (check)
            {
                Frm_EmployeeSalary frm = new Frm_EmployeeSalary();
                frm.ShowDialog();
            }
                
        }

        private void btnEmployeeborrowReports_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Borrow_ItemsReport", "User_Emp");
            if (check)
            {
                Frm_EmployeeBorrowItemsReport frm = new Frm_EmployeeBorrowItemsReport();
                frm.ShowDialog();
            }
                
        }

        private void btnEmpBorrowMoneyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Borrow_MoneyReport", "User_Emp");
            if (check)
            {
                Frm_EmployeeBorrowMoneyReport frm = new Frm_EmployeeBorrowMoneyReport();
                frm.ShowDialog();
            }
                
        }

        private void btnEmpSalaryReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("SalaryReport", "User_Emp");
            if (check)
            {
                Frm_EmployeeSalaryReport frm = new Frm_EmployeeSalaryReport();
                frm.ShowDialog();
            }
                

        }
        private bool checkUser(string field,string table)
        {
            DataTable tblSearch = new DataTable();
            tblSearch = db.readData("select "+field+" from "+table+" where User_ID=" + Properties.Settings.Default.User_ID + "", "");
            if (Convert.ToDecimal(tblSearch.Rows[0][0]) == 0)
            {
                MessageBox.Show("انت لا تملك صلاحيه الدخول لهذه الشاشة", "تاكيد", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnUnits_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Unit", "User_Setting");
            if (check)
            {
                Frm_Unit frm = new Frm_Unit();
                frm.ShowDialog();
            }
            
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Item_Group", "User_Setting");
            if (check)
            {
                Frm_ProductGroup frm = new Frm_ProductGroup();
                frm.ShowDialog();
            }       
        }

        private void btnStore_Click(object sender, EventArgs e)
        {
            
        }



        private void btnProgramSetting_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Setting", "User_Setting");
            if (check)
            {
                Frm_Setting frm = new Frm_Setting();
                frm.ShowDialog();
            }
               
        }

        private void btnShowProducts_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Item_View", "User_Setting");
            if (check)
            {
                Frm_ViewItem frm = new Frm_ViewItem();
                frm.ShowDialog();
            }
                
        }

        private void btnAddNewStore_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_Add", "User_Setting");
            if (check)
            {
                Frm_Store frm = new Frm_Store();
                frm.ShowDialog();
            }
                
        }

        private void btnStoreGard_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_Gard", "User_Setting");
            if (check)
            {
                Frm_StoreGard frm = new Frm_StoreGard();
                frm.ShowDialog();
            }
                
        }

        private void btnStoreTransfire_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_Transfire", "User_Setting");
            if (check)
            {
                Frm_StoreTransfire frm = new Frm_StoreTransfire();
                frm.ShowDialog();
            }
                
        }

        private void btnStoreTransfireReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_TransfireReport", "User_Setting");
            if (check)
            {
                Frm_StoreTransfireReport frm = new Frm_StoreTransfireReport();
                frm.ShowDialog();
            }
                
        }

        private void btnProdcutOutStore_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_Out", "User_Setting");
            if (check)
            {
                Frm_ProdcutsOutStore frm = new Frm_ProdcutsOutStore();
                frm.ShowDialog();
            }
                
        }

        private void btnProdcutOutStoreReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("Store_OutReport", "User_Setting");
            if (check)
            {
                Frm_ProdcutsOutStoreReport frm = new Frm_ProdcutsOutStoreReport();
                frm.ShowDialog();
            }
                
        }

        private void btnTaxReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("TaxesReport", "User_Deserved");
            if (check)
            {
                Frm_TaxesReport frm = new Frm_TaxesReport();
                frm.ShowDialog();
            }
                
        }

        private void btnUsersPermissions_Click(object sender, EventArgs e)
        {
            bool check = checkUser("User_Permisson", "User_Setting");
            if (check)
            {
                Frm_Permission frm = new Frm_Permission();
                frm.ShowDialog();
            }
                
        }

        private void btnProductRequired_Click(object sender, EventArgs e)
        {
            bool check = checkUser("ProductRequired", "User_Setting");
            if (check)
            {
                Frm_ProductRequired frm = new Frm_ProductRequired();
                frm.ShowDialog();
            }
            
        }

        private void btnDeletedSalesReport_Click(object sender, EventArgs e)
        {

            bool check = checkUser("SaleDeletedReport", "User_Sale");
            if (check)
            {
                Frm_Sales_Deleted_Report frm = new Frm_Sales_Deleted_Report();
                frm.ShowDialog();
            }
        }

        private void btnDeletedBuyReport_Click(object sender, EventArgs e)
        {
            bool check = checkUser("BuyDeletedReport", "User_Buy");
            if (check)
            {
                Frm_Buy_Deleted_Report frm = new Frm_Buy_Deleted_Report();
                frm.ShowDialog();
            }
        }
    }
}
