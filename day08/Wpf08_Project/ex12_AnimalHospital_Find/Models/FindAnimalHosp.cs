namespace ex12_AnimalHospital_Find.Models
{
    internal class FindAnimalHosp
    {

        public string Gugun { get; set; }
        public string Animal_hospital {  get; set; }
        public string Approval {  get; set; }
        public string Road_address {  get; set; }
        public string Tel {  get; set; }
        public double Lat {  get; set; }
        public double Lon { get; set; }
        public string Basic_date {  get; set; }

        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[FindAnimal_Hp]
                                                                   ([gugun]
                                                                   ,[animal_hospital]
                                                                   ,[approval]
                                                                   ,[road_address]
                                                                   ,[tel]
                                                                   ,[lat]
                                                                   ,[lon]
                                                                   ,[basic_data])
                                                             VALUES
                                                                   (@gugun
                                                                   ,@animal_hospital
                                                                   ,@approval
                                                                   ,@road_address
                                                                   ,@tel
                                                                   ,@lat
                                                                   ,@lon
                                                                   ,@basic_data)";
        public static readonly string SELECT_QUERY = @"SELECT [Num]
                                                              ,[gugun]
                                                              ,[animal_hospital]
                                                              ,[approval]
                                                              ,[road_address]
                                                              ,[tel]
                                                              ,[lat]
                                                              ,[lon]
                                                              ,[basic_data]
                                                          FROM [dbo].[FindAnimal_Hp]";

        public static readonly string SELECT_QUERY_BY_GUGUN = @"SELECT [gugun]
                                                                ,[animal_hospital]
                                                                ,[approval]
                                                                ,[road_address]
                                                                ,[tel]
                                                                ,[lat]
                                                                ,[lon]
                                                                ,[basic_data]
                                                          FROM [dbo].[FindAnimal_Hp]
                                                          WHERE [gugun] = @gugun";

        public static readonly string GETDATE_QUERY = @"SELECT DISTINCT gugun
                                                          FROM [dbo].[FindAnimal_Hp]";
    }
}
