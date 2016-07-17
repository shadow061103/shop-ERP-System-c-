using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication9
{
    public partial class Form1 : Form
    {
        SqlConnectionStringBuilder scsb;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO:  這行程式碼會將資料載入 'project1DataSet.Product' 資料表。您可以視需要進行移動或移除。
            this.productTableAdapter.Fill(this.project1DataSet.Product);
             scsb = new SqlConnectionStringBuilder();
           
            //scsb.DataSource = "CR1-16";
            scsb.DataSource = "KUANFU-PC\\SQLEXPRESS";
            scsb.InitialCatalog = "Project1";
            scsb.IntegratedSecurity = true;

            showDataGridView2();//產品資料表
            showDataGridView4();//客戶資料
            showDataGridView1();//訂單主檔
            showDataGridView5();//訂單明細
        }

        private void btnO第一筆_Click(object sender, EventArgs e)
        {
           
        }

        private void btnO上一筆_Click(object sender, EventArgs e)
        {

        }

        private void btnO下一筆_Click(object sender, EventArgs e)
        {

        }

        private void btnO最後一筆_Click(object sender, EventArgs e)
        {

        }
   

        private void btnO新增_Click(object sender, EventArgs e)
        {
           
            if ((cboxAR.Text.Length>0) && (cboxorder_status.Text.Length>0) &&(cboxpaymethod.Text.Length >0) )
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into OrderMaster values (@Neworderdata,@Newshipdate,@shipcheckstatus,@Newreceiver,@Newphone,@Newpost,@NewAddress,@NewEmail,@Newfreight,@Newpaymethod,@NewAr ,@Neworder_status,@Newclosedate)";
                SqlCommand cmd = new SqlCommand(strSQL, con);


                cmd.Parameters.AddWithValue(@"Neworderdata", (DateTime)dtporderdata.Value);
                cmd.Parameters.AddWithValue(@"Newshipdate", (DateTime)dtpshipdate.Value);
                cmd.Parameters.AddWithValue(@"shipcheckstatus", cboxshipcheckstatus.Text);
                cmd.Parameters.AddWithValue(@"Newreceiver", tbreceiver.Text);
                cmd.Parameters.AddWithValue(@"Newphone", tbreceiverphone.Text);
                cmd.Parameters.AddWithValue(@"Newpost", tbreceiverpost.Text);
                cmd.Parameters.AddWithValue(@"NewAddress", tbreceiveraddress.Text);
                cmd.Parameters.AddWithValue(@"NewEmail", tbreceiveremail.Text);
                cmd.Parameters.AddWithValue(@"Newfreight", tbfreight.Text);
                cmd.Parameters.AddWithValue(@"Newpaymethod", cboxpaymethod.Text);
                cmd.Parameters.AddWithValue(@"NewAr", cboxAR.Text);
                cmd.Parameters.AddWithValue(@"Neworder_status", cboxorder_status.Text);
                cmd.Parameters.AddWithValue(@"Newclosedate", (DateTime)dtpclosedate.Value);
                

                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView1();
            }
            else
            {
                MessageBox.Show("請選擇付款方式，是否收款，訂單結案狀態");


            }
        }

        private void btnO修改_Click(object sender, EventArgs e)
        {
            if ((cboxAR.Text.Length > 0) && (cboxorder_status.Text.Length > 0) && (cboxpaymethod.Text.Length > 0))
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "update OrderMaster set order_date=@Neworderdata,"
                +"order_shipdate=@Newshipdate,order_shipcheckstatus=@shipcheckstatus,"
                +"order_receiver=@Newreceiver,order_phone=@Newphone,receiver_post=@Newpost,"
                +"receiver_address=@NewAddress,receiver_email=@NewEmail,freight_fee=@Newfreight"
                +",pay_method=@Newpaymethod,account_receive=@NewAr ,order_status=@Neworder_status"
                + ",order_closedate=@Newclosedate where order_no=@orderno";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"Neworderdata", (DateTime)dtporderdata.Value);
                cmd.Parameters.AddWithValue(@"Newshipdate", (DateTime)dtpshipdate.Value);
                cmd.Parameters.AddWithValue(@"shipcheckstatus", cboxshipcheckstatus.Text);
                cmd.Parameters.AddWithValue(@"Newreceiver", tbreceiver.Text);
                cmd.Parameters.AddWithValue(@"Newphone", tbreceiverphone.Text);
                cmd.Parameters.AddWithValue(@"Newpost", tbreceiverpost.Text);
                cmd.Parameters.AddWithValue(@"NewAddress", tbreceiveraddress.Text);
                cmd.Parameters.AddWithValue(@"NewEmail", tbreceiveremail.Text);
                cmd.Parameters.AddWithValue(@"Newfreight", tbfreight.Text);
                cmd.Parameters.AddWithValue(@"Newpaymethod", cboxpaymethod.Text);
                cmd.Parameters.AddWithValue(@"NewAr", cboxAR.Text);
                cmd.Parameters.AddWithValue(@"Neworder_status", cboxorder_status.Text);
                cmd.Parameters.AddWithValue(@"Newclosedate", (DateTime)dtpclosedate.Value);
                cmd.Parameters.AddWithValue(@"orderno", tborder_no.Text);

                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView1();
            }
            else
            {
                MessageBox.Show("請選擇付款方式，是否收款，訂單結案狀態");


            }
        }

        private void btnO刪除_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "delete from OrderMaster where order_no=@orderno";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue(@"orderno", tborder_no.Text);

            int rows = cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(String.Format("資料刪除完畢,共影響{0}筆資料", rows));


            tborder_no.Text = "";
            tbfreight.Text = "";
            dtporderdata.Value = DateTime.Now;
            cboxpaymethod.Text = "";
            cboxorder_status.Text = "";
            tbreceiver.Text = "";
            tbreceiveraddress.Text = "";
            tbreceiverphone.Text = "";
            tbreceiverpost.Text = "";
            tbreceiveremail.Text = "";
            cboxAR.Text = "";
            cboxshipcheckstatus.Text = "";
            dtpshipdate.Value = DateTime.Now;
            dtpclosedate.Value = DateTime.Now;
            showDataGridView1();
           
        }

       

        private void btnO查詢_Click(object sender, EventArgs e)
        {

        }

        private void btnC新增_Click(object sender, EventArgs e)
        {
            if (tbcustomer.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into customer values(@customer_name,@customer_post,@customer_address,@customer_email,@customer_phone ) ";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"customer_name", tbcustomer.Text);
                cmd.Parameters.AddWithValue(@"customer_post", tbcustomerpost.Text);
                cmd.Parameters.AddWithValue(@"customer_address", tbcustomeraddress.Text);
                cmd.Parameters.AddWithValue(@"customer_email", tbcustomeremail.Text);
                cmd.Parameters.AddWithValue(@"customer_phone", tbcustomerphone.Text);

                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView4();
            }
            else
            {
                MessageBox.Show("請輸入客戶姓名");


            }
        }

        private void btnC修改_Click(object sender, EventArgs e)
        {//
            if (tbcustomer.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
              string strSQL = "update customer set  customer_phone=@Newcustomerphone,"
                + "customer_post=@Newcustomerpost,customer_address=@Newcustomeraddress,"
                + "customer_email=@Newcustomeremail where customer_name=@Searchname";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"Searchname", tbcustomer.Text);
                cmd.Parameters.AddWithValue(@"Newcustomername", tbcustomer.Text);
                cmd.Parameters.AddWithValue(@"Newcustomerpost", tbcustomerpost.Text);
                cmd.Parameters.AddWithValue(@"Newcustomeraddress", tbcustomeraddress.Text);
                cmd.Parameters.AddWithValue(@"Newcustomeremail", tbcustomeremail.Text);
                cmd.Parameters.AddWithValue(@"Newcustomerphone", tbcustomerphone.Text);

                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView4();
            }
            else
            {
                MessageBox.Show("請輸入姓名");


            }
        }

        private void btnC刪除_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "delete from customer where customer_phone=@Oldphone";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@Oldphone", tbcustomerphone.Text);

            int rows = cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(String.Format("資料刪除完畢,共影響{0}筆資料", rows));
            tbcustomer.Text = "";
            tbcustomeraddress.Text = "";
            tbcustomeremail.Text = "";
            tbcustomerphone.Text = "";
            tbcustomerpost.Text = "";
            showDataGridView4();
        }

        private void btnC查詢_Click(object sender, EventArgs e)
        {
            if (tbcustomer.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select*from customer where customer_name like @searchname";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@searchname", "%" + tbcustomer.Text + "%");


                SqlDataReader reader = cmd.ExecuteReader();
                showDataGridView4_1();
                if (reader.Read())//有讀到資料
                {
                    tbcustomer.Text = String.Format("{0}", reader["customer_name"]);
                    tbcustomerpost.Text = String.Format("{0}", reader["customer_post"]);
                    tbcustomeraddress.Text = String.Format("{0}", reader["customer_address"]);
                    tbcustomeremail.Text = String.Format("{0}", reader["customer_email"]);
                    tbcustomerphone.Text = String.Format("{0}", reader["customer_phone"]);



                }
                else
                {
                    MessageBox.Show("查無此人!!");
                    tbcustomer.Text = "";
                    tbcustomeraddress.Text = "";
                    tbcustomeremail.Text = "";
                    tbcustomerphone.Text = "";
                    tbcustomerpost.Text = "";

                }
                reader.Close();
                con.Close();

            }
            else
            {
                MessageBox.Show("請輸入姓名搜尋");
            }

        }

        private void btnP新增_Click(object sender, EventArgs e)
        {
            if (tbproductname.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into Product values(@productname,@productspec,@productcost,@productprice ) ";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"productname", tbproductname.Text);
                cmd.Parameters.AddWithValue(@"productspec", tbproductspec.Text);
                cmd.Parameters.AddWithValue(@"productcost", tbproductcost.Text);
                cmd.Parameters.AddWithValue(@"productprice", tbproductprice.Text);
                
                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView2();
            }
            else
            {
                MessageBox.Show("請輸入產品名稱");


            }
        }

        private void btnP修改_Click(object sender, EventArgs e)
        {
            if (tbproductname.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "update Product set  product_spec=@Newproductspec,"
                    + "product_cost=@Newproductcost, product_price=@Newproductprice"
                    + " where product_name=@Searchname";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"Searchname", tbproductname.Text);
                cmd.Parameters.AddWithValue(@"Newproductspec", tbproductspec.Text);
                cmd.Parameters.AddWithValue(@"Newproductcost", tbproductcost.Text);
                cmd.Parameters.AddWithValue(@"Newproductprice", tbproductprice.Text);
               
                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView2();
            }
            else
            {
                MessageBox.Show("請輸入產品名稱");


            }
        }

        private void btnP刪除_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "delete from Product where product_name=@OldName";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue("@OldName", tbproductname.Text);

            int rows = cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(String.Format("資料刪除完畢,共影響{0}筆資料", rows));

            tbproductname.Text = "";
            tbproductspec.Text = "";
            tbproductcost.Text = "";
            tbproductprice.Text = "";
            showDataGridView2();
            /*將產品編號重置但要全部刪除資料
            string strSQL2 = "DBCC CHECKIDENT ('Product', RESEED, 0)";
            SqlCommand cmd2 = new SqlCommand(strSQL2, con);*/



        }

       

        private void btnP查詢_Click(object sender, EventArgs e)
        {
            if (tbproductname.Text.Length > 0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select*from Product where product_name like @searchname";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@searchname", "%" + tbproductname.Text + "%");  
                
                SqlDataReader reader = cmd.ExecuteReader();
                showDataGridView2_1();
                if (reader.Read())//有讀到資料
                {
                    tbproductno.Text = String.Format("{0}", reader["product_no"]);
                    tbproductname.Text = String.Format("{0}", reader["product_name"]);
                    tbproductspec.Text = String.Format("{0}", reader["product_spec"]);
                    tbproductcost.Text = String.Format("{0}", reader["product_cost"]);
                    tbproductprice.Text = String.Format("{0}", reader["product_price"]);
               


                }
                else
                {
                    MessageBox.Show("查無產品!!");
                   tbproductno.Text="";
                   tbproductspec.Text = "";
                   tbproductprice.Text = "";
                   tbproductcost.Text = "";
                   tbproductname.Text = "";

                }
                reader.Close();
                con.Close();

            }
            else
            {
                MessageBox.Show("請輸入姓名搜尋");
            }

        }
        private void showDataGridView2()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select product_no as 產品編號,product_name as 產品名稱,"
            +"product_spec as 規格,product_cost as 產品成本,"
            +"product_price as 產品價格  from product";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView2.DataSource = dt;
            }
           reader.Close();
            con.Close();

        }
        private void showDataGridView2_1()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Product where product_name like  '%"+tbproductname.Text+"%'";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView2.DataSource = dt;
            }
            reader.Close();
            con.Close();

        }
        private void showDataGridView4()
        {//客戶資料表
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select customer_no as 客戶編號,customer_name as 客戶姓名,"
            +"customer_post as 郵遞區號,customer_address as 地址,"
            +"customer_email as Email,customer_phone as 手機 from customer";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable ds = new DataTable();
                ds.Load(reader);
                dataGridView4.DataSource = ds;
            }
            reader.Close();
            con.Close();

        }
        private void showDataGridView5()
        {//訂單主檔
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select order_no as 訂單編號,product_no as 產品編號,unitprice as 單價,"
            +"order_qty  as 訂購數量,order_shipqty as 出貨數量,order_totalcost as 小計 from OrderDetail ";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable ds = new DataTable();
                ds.Load(reader);
                dataGridView5.DataSource = ds;
            }
            reader.Close();
            con.Close();

        }
        private void showDataGridView1()
        {//訂單主檔
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select order_no as 訂單編號,order_date as 訂單日期,order_shipdate as 訂單出貨日,order_shipcheckstatus as 物流出貨確認狀態,order_receiver as 收貨人,order_phone as 收貨人手機,"
                       +"receiver_post as 收貨人郵遞區號,receiver_address as 收貨人地址,receiver_email as 收貨人email,freight_fee as 物流費用,pay_method as 付款方式,account_receive as 是否收款,order_status as 訂單結案狀態,"
           +" order_closedate as 訂單結案日期 from OrderMaster ";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable ds = new DataTable();
                ds.Load(reader);
                dataGridView1.DataSource = ds;
            }
            reader.Close();
            con.Close();

        }
        private void showDataGridView1_1()
        {//訂單主檔
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select order_no as 訂單編號,order_date as 訂單日期,order_shipdate as 訂單出貨日,order_shipcheckstatus as 物流出貨確認狀態,order_receiver as 收貨人,order_phone as 收貨人手機,"
                       + "receiver_post as 收貨人郵遞區號,receiver_address as 收貨人地址,receiver_email as 收貨人email,freight_fee as 物流費用,pay_method as 付款方式,account_receive as 是否收款,order_status as 訂單結案狀態,"
                      + " order_closedate as 訂單結案日期 from OrderMaster where order_no=@orderno ";
            SqlCommand cmd = new SqlCommand(strSQL, con);
            cmd.Parameters.AddWithValue(@"orderno", tborder_no.Text);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable ds = new DataTable();
                ds.Load(reader);
                dataGridView1.DataSource = ds;
            }
            reader.Close();
            con.Close();

        }
        private void showDataGridView4_1()
        {
            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select customer_no as 客戶編號,customer_name as 客戶姓名,"
            + "customer_post as 郵遞區號,customer_address as 地址,"
            + "customer_email as Email,customer_phone as 手機 from customer where customer_name like  '%" + tbcustomer.Text + "%'";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView4.DataSource = dt;
            }
            reader.Close();
            con.Close();

        }
        private void productgridview_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            string strQueryID = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from Product where product_no=@QUERYID";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            cmd.Parameters.AddWithValue(@"QUERYID", strQueryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tbproductno.Text = String.Format("{0}", reader["product_no"]);
                tbproductname.Text = String.Format("{0}", reader["product_name"]);
                tbproductspec.Text = String.Format("{0}", reader["product_spec"]);
                tbproductcost.Text = String.Format("{0}", reader["product_cost"]);
                tbproductprice.Text = String.Format("{0}", reader["product_price"]);
               

            }
            reader.Close();
            con.Close();
        }

        private void btnP清空_Click(object sender, EventArgs e)
        {
            tbproductno.Text = "";
            tbproductspec.Text = "";
            tbproductprice.Text = "";
            tbproductcost.Text = "";
            tbproductname.Text = "";
            showDataGridView2();
        }

        private void btnC清空_Click(object sender, EventArgs e)
        {
            tbcustomer.Text = "";
            tbcustomeraddress.Text = "";
            tbcustomeremail.Text = "";
            tbcustomerphone.Text = "";
            tbcustomerpost.Text = "";
            showDataGridView4();
        }

        private void customergridview_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            string strQueryID = dataGridView4.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from customer where customer_no=@QUERYID";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            cmd.Parameters.AddWithValue(@"QUERYID", strQueryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tbcustomer.Text = String.Format("{0}", reader["customer_name"]);
                tbcustomerpost.Text = String.Format("{0}", reader["customer_post"]);
                tbcustomeraddress.Text = String.Format("{0}", reader["customer_address"]);
                tbcustomeremail.Text = String.Format("{0}", reader["customer_email"]);
                tbcustomerphone.Text = String.Format("{0}", reader["customer_phone"]);


            }
            reader.Close();
            con.Close();
        }

        private void datagridview_cellcheck(object sender, DataGridViewCellEventArgs e)
        {
            string strQueryID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from OrderMaster where order_no=@QUERYID";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            cmd.Parameters.AddWithValue(@"QUERYID", strQueryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tborder_no.Text = String.Format("{0}", reader["order_no"]);
                tbfreight.Text = String.Format("{0}", reader["freight_fee"]);
                dtporderdata.Value = (DateTime)reader["order_date"];
                cboxpaymethod.Text = String.Format("{0}", reader["pay_method"]);
                cboxorder_status.Text = String.Format("{0}", reader["order_status"]);
                tbreceiver.Text = String.Format("{0}", reader["order_receiver"]);
                tbreceiveraddress.Text = String.Format("{0}", reader["receiver_address"]);
                tbreceiverphone.Text = String.Format("{0}", reader["order_phone"]);
                tbreceiverpost.Text = String.Format("{0}", reader["receiver_post"]);
                tbreceiveremail.Text = String.Format("{0}", reader["receiver_email"]);
                cboxAR.Text=String.Format("{0}", reader["account_receive"]);
                cboxshipcheckstatus.Text=String.Format("{0}", reader["order_shipcheckstatus"]);
                dtpshipdate.Value = (DateTime)reader["order_shipdate"];
                dtpclosedate.Value = (DateTime)reader["order_closedate"];              
            }
            reader.Close();
            con.Close();
        }

        private void btn查詢_Click(object sender, EventArgs e)
        {
            if (tborder_no.Text.Length>0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select*from OrderMaster where order_no=@orderno";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"orderno", tborder_no.Text);


                SqlDataReader reader = cmd.ExecuteReader();
                showDataGridView1_1();
                if (reader.Read())//有讀到資料
                {


                    tborder_no.Text = String.Format("{0}", reader["order_no"]);
                    tbfreight.Text = String.Format("{0}", reader["freight_fee"]);
                    dtporderdata.Value = (DateTime)reader["order_date"];
                    cboxpaymethod.Text = String.Format("{0}", reader["pay_method"]);
                    cboxorder_status.Text = String.Format("{0}", reader["order_status"]);
                    tbreceiver.Text = String.Format("{0}", reader["order_receiver"]);
                    tbreceiveraddress.Text = String.Format("{0}", reader["receiver_address"]);
                    tbreceiverphone.Text = String.Format("{0}", reader["order_phone"]);
                    tbreceiverpost.Text = String.Format("{0}", reader["receiver_post"]);
                    tbreceiveremail.Text = String.Format("{0}", reader["receiver_email"]);
                    cboxAR.Text = String.Format("{0}", reader["account_receive"]);
                    cboxshipcheckstatus.Text = String.Format("{0}", reader["order_shipcheckstatus"]);
                    dtpshipdate.Value = (DateTime)reader["order_shipdate"];
                    dtpclosedate.Value = (DateTime)reader["order_closedate"];

                }
                    
                else
                {
                    MessageBox.Show("查無此訂單!!");
                    

                }
                reader.Close();
                con.Close();

            }
            else
            {
                MessageBox.Show("請輸入訂單編號搜尋");
            }
        }

        private void btn清空_Click(object sender, EventArgs e)
        {
            tborder_no.Text = "";
            tbfreight.Text = "";
            dtporderdata.Value = DateTime.Now;
            cboxpaymethod.Text = "";
            cboxorder_status.Text = "";
            tbreceiver.Text = "";
            tbreceiveraddress.Text = "";
            tbreceiverphone.Text = "";
            tbreceiverpost.Text = "";
            tbreceiveremail.Text = "";
            cboxAR.Text = "";
            cboxshipcheckstatus.Text = "";
            dtpshipdate.Value = DateTime.Now;
            dtpclosedate.Value = DateTime.Now;
            showDataGridView1();
        }

        private void btnO加入常客資料_Click(object sender, EventArgs e)
        {
            if (tbreceiverphone.Text.Length>0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select*from customer where customer_phone = @searchphone";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"searchphone", tbreceiverphone.Text);


                SqlDataReader reader = cmd.ExecuteReader();
               
                if (reader.Read())//有讀到資料
                {
                    tbreceiver.Text = String.Format("{0}", reader["customer_name"]);
                    tbreceiverpost.Text = String.Format("{0}", reader["customer_post"]);
                    tbreceiveraddress.Text = String.Format("{0}", reader["customer_address"]);
                    tbreceiveremail.Text = String.Format("{0}", reader["customer_email"]);
                    tbreceiverphone.Text = String.Format("{0}", reader["customer_phone"]);



                }
                else
                {
                    MessageBox.Show("查無此人!!");
                    tbreceiver.Text = "";
                    tbreceiverpost.Text = "";
                    tbreceiveraddress.Text ="";
                    tbreceiveremail.Text = "";
                    tbreceiverphone.Text = "";



                }
                reader.Close();
                con.Close();

            }
            else
            {
                MessageBox.Show("請輸入正確手機號碼");
            }

        }

        private void detail_cellclick(object sender, DataGridViewCellEventArgs e)
        {
            string strQueryID = dataGridView5.Rows[e.RowIndex].Cells[0].Value.ToString();

            SqlConnection con = new SqlConnection(scsb.ToString());
            con.Open();
            string strSQL = "select*from OrderDetail where order_no=@QUERYID";
            SqlCommand cmd = new SqlCommand(strSQL, con);

            cmd.Parameters.AddWithValue(@"QUERYID", strQueryID);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                tbDPp_no.Text = String.Format("{0}", reader["product_no"]);
                tbDPpname.Text = String.Format("{0}", reader["product_name"]);
                tbDPprice.Text = String.Format("{0}", reader["unitprice"]);
                tbDPorderqty.Text = String.Format("{0}", reader["order_qty"]);
                tbDPshipqty.Text = String.Format("{0}", reader["order_shipqty"]);

               
                
            }
            reader.Close();
            con.Close();
        }

        private void datail_selectchange(object sender, EventArgs e)
        {
            if (cboxDPpname.Text.Length>0)
            {
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "select*from Product where product_name=@searchname";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue("@searchname", cboxDPpname.Text);

                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())//有讀到資料
                {


                  tbDPp_no.Text = String.Format("{0}", reader["product_no"]);
                  tbDPpname.Text = String.Format("{0}", reader["product_name"]);
                  tbDPprice.Text = String.Format("{0}", reader["product_price"]);



                }
                else
                {
                    MessageBox.Show("查無產品!!");
                   tbDPp_no.Text = "";
                  tbDPpname.Text = "";
                  tbDPprice.Text = "";


                }
                reader.Close();
               con.Close();

            }
            
        }

        private void btnDP新增_Click(object sender, EventArgs e)
        {
            if ((tbDPp_no.Text.Length > 0) && (tborder_no.Text.Length > 0) && (tbDPorderqty.Text.Length > 0) && (tbDPshipqty.Text.Length > 0))
            {
                double total = 0;
                double price = Convert.ToDouble(tbDPprice.Text);
                double qty = Convert.ToDouble(tbDPshipqty.Text);
                total = price * qty;
                SqlConnection con = new SqlConnection(scsb.ToString());
                con.Open();
                string strSQL = "insert into OrderDetail values(@orderno,@productno,@productname,@unitprice,@orderqty,@ordershipqty,@total) ";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.AddWithValue(@"orderno", tborder_no.Text);
                cmd.Parameters.AddWithValue(@"productno", tbDPp_no.Text);
                cmd.Parameters.AddWithValue(@"productname", tbDPpname.Text);
                cmd.Parameters.AddWithValue(@"unitprice", tbDPprice.Text);
                cmd.Parameters.AddWithValue(@"orderqty", tbDPorderqty.Text);
                cmd.Parameters.AddWithValue(@"ordershipqty", tbDPshipqty.Text);
                cmd.Parameters.AddWithValue(@"total", total);

                int rows = cmd.ExecuteNonQuery();//執行但不查詢  會回傳整數值(異動幾筆資料)
                con.Close();
                MessageBox.Show(String.Format("資料更新完畢,共影響{0}筆資料", rows));
                showDataGridView5();
            }
            else
            {
                MessageBox.Show("請選擇欲新增的產品");


            }

        }
    }
}
