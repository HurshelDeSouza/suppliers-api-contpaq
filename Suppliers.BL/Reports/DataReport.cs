namespace Suppliers.BL.Reports;

public class DataReport
{
    public string CreatedAt { get; set; } = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    public string DateRange { get; set; } = "";
}
