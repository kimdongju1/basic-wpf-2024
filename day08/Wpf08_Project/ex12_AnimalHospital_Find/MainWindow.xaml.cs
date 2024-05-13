using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ex12_AnimalHospital_Find;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Newtonsoft.Json.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using System.Diagnostics;
using ex12_AnimalHospital_Find.Models;
using Microsoft.VisualBasic.Logging;

namespace ex12_AnimalHospital_Find
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        
        
            
        

        private async void BtnReqRealtime_Click(object sender, RoutedEventArgs e)
        {
            string openApiUri = "https://apis.data.go.kr/6260000/BusanAnimalHospService/getTblAnimalHospital?serviceKey=dBZKviyzKBEggoDlDtv%2F5D3SxdJJR5WRsISUD4vmLMb9%2BHcE0MPllCFf6FCGi7ureBfBEhXt5RZrrqTK48nCWA%3D%3D&pageNo=1&numOfRows=10&resultType=json";
            string result = string.Empty;

            WebRequest req = null;
            WebResponse res = null;
            StreamReader reader = null;

            try
            {
                req = WebRequest.Create(openApiUri);
                res = await req.GetResponseAsync();
                reader = new StreamReader(res.GetResponseStream());
                result = reader.ReadToEnd();

                Debug.WriteLine(result);
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("오류", $"OpenAPI 조회오류 {ex.Message}");
            }

            var jsonResult = JObject.Parse(result);
            var status = Convert.ToInt32(jsonResult["status"]);

            if (status == 200)
            {
                var data = jsonResult["data"];
                var jsonArray = data as JArray;

                var findAnimalHosps = new List<FindAnimalHosp>();
                foreach (var item in jsonArray)
                {
                    findAnimalHosps.Add(new FindAnimalHosp()
                    {
                        Id = 0,
                        Gugun = Convert.ToString(item["gugun"]),
                        Animal_hospital = Convert.ToString(item["animal_hospital"]),
                        Approval = Convert.ToString(item["approval"]),
                        Road_address = Convert.ToString(item["road_address"]),
                        Tel = Convert.ToString(item["tel"]),
                        Lat = Convert.ToString(item["lat"]),
                        Lon = Convert.ToString(item["lon"]),
                        Basic_date = Convert.ToString(item["basic_data"]),
                    });

                    this.DataContext = findAnimalHosps;
                    //StsResult.Content = $"OpenAPI {findAnimalHosps.Count}건 조회완료!";
                    
                }

            }
        }

        private async void BtnSaveData_Click(object sender, RoutedEventArgs e)
        {
            if (GrdResult.Items.Count == 0)
            {
                await this.ShowMessageAsync("저장오류", "실시간 조회후 저장하십시오.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
                {
                    conn.Open();

                    var insRes = 0;
                    foreach (FindAnimalHosp item in GrdResult.Items)
                    {
                        SqlCommand cmd = new SqlCommand(Models.FindAnimalHosp.INSERT_QUERY, conn);
                        cmd.Parameters.AddWithValue("@Gugun", item.Gugun);
                        cmd.Parameters.AddWithValue("@Animal_hospital", item.Animal_hospital);
                        cmd.Parameters.AddWithValue("@Approval", item.Approval);
                        cmd.Parameters.AddWithValue("@Road_address", item.Road_address);
                        cmd.Parameters.AddWithValue("@Tel", item.Tel);
                        cmd.Parameters.AddWithValue("@Lat", item.Lat);
                        cmd.Parameters.AddWithValue("@Lon", item.Lon);
                        cmd.Parameters.AddWithValue("@Basic_date", item.Basic_date);

                        insRes += cmd.ExecuteNonQuery();
                    }

                    if (insRes >0)
                    {
                        await this.ShowMessageAsync("저장", "DB저장성공!");
                        //StsResult.Content = $"DB저장 {insRes}건 성공!";
                    }
                }
            }
            catch (Exception ex)
            {
                await this.ShowMessageAsync("저장오류", $"저장오류 {ex.Message}");
                
            }

            
        }

        private void CboReqDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    if (CboReqDate.SelectedValue != null)
        //    {
        //        using (SqlConnection conn = new SqlConnection(Helpers.Common.CONNSTRING))
        //        {
        //            conn.Open();

        //            SqlCommand cmd = new SqlCommand(Models.FindAnimalHosp.SELECT_QUERY, conn);
        //            cmd.Parameters.AddWithValue("@basic_data", CboReqDate.SelectedValue.ToString());
        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataSet dSet = new DataSet();
        //            adapter.Fill(dSet, "FindAnimalHosp");
        //            var findAnimalHosps = new List<FindAnimalHosp>();

        //            foreach (DataRow row in dSet.Tables["FindAnimalHosp"].Rows)
        //            {
        //                FindAnimalHosp.Add(new FindAnimalHosp
        //                {
        //                    Id = 0,
        //                    Gugun = Convert.ToString(row["gugun"]),
        //                    Animal_hospital = Convert.ToString(row["animal_hospital"]),
        //                    Approval = Convert.ToString(row["approval"]),
        //                    Road_address = Convert.ToString(row["road_address"]),
        //                    Tel = Convert.ToString(row["tel"]),
        //                    Lat = Convert.ToString(row["lat"]),
        //                    Lon = Convert.ToString(row["lon"]),
        //                    Basic_date = Convert.ToString(row["basic_data"]),
        //                });
        //            }

        //            this.DataContext = findAnimalHosps;
        //            StsResult.Content = $"DB {findAnimalHosps.Count}건 조회완료";
        //        }
        //    }
        //    else
        //    {
        //        this.DataContext = null;
        //        StsResult.Content = $"DB 조회클리어";
        //    }
        }

        
        private void GrdResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var curItem = GrdResult.SelectedItem as FindAnimalHosp;

            var mapWindow = new MapWindow(curItem.Lat, curItem.Lon);
            mapWindow.Owner = this;
            mapWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            mapWindow.ShowDialog();

        }

        
    }
}