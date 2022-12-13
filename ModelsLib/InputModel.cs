using System.ComponentModel.DataAnnotations;

namespace ModelsLib;

public class InputModel
{
    [Required] 
    public string Id { get; set; }
    [Required]
    public string Account { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Site { get; set; }
    [Required]
    public string State { get; set; }
    [Required] 
    public string SavePath { get; set; }
    public string Date { get; set; } = DateTime.Now.Date.ToString();
}