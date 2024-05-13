namespace ex12_AnimalHospital_Find.Models
{
    internal class FindAnimalHosp
    {
        public int Id { get; set; }
        public string Gugun { get; set; }
        public string Animal_hospital {  get; set; }
        public string Approval {  get; set; }
        public string Road_address {  get; set; }
        public string Tel {  get; set; }
        public string Lat {  get; set; }
        public string Lon { get; set; }
        public string Basic_date {  get; set; }

        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[FindAnimalHosp] 
                                                                   ([Id]
                                                                  ,[Gugun]
                                                                  ,[Animal_hospital]
                                                                  ,[Approval]
                                                                  ,[Road_address]
                                                                  ,[Tel]
                                                                  ,[Lat]
                                                                  ,[Lon]
                                                                  ,[Basic_date])
                                                            VALUES
                                                                  (@Id
                                                                  ,@Gugun
                                                                  ,@Animal_hospital
                                                                  ,@Approval
                                                                  ,@Road_address
                                                                  ,@Tel
                                                                  ,@Lat
                                                                  ,@Lon
                                                                  ,@Basic_date)";
        public static readonly string SELECT_QUERY = @"SELECT [Id]
                                                              ,[Gugun]
                                                              ,[Animal_hospital]
                                                              ,[Approval]
                                                              ,[Road_address]
                                                              ,[Tel]
                                                              ,[Lat]
                                                              ,[Lon]
                                                              ,[Basic_date]
                                                          FROM [dbo].[FindAnimalHosp]
                                                         WHERE CONVERT(CHAR(10), [Basic_date], 23) = @Basic_date";
        

    }
}
