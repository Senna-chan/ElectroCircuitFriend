using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ElectroCircuitFriendRemake.Models;
using Microsoft.AspNetCore.Http;

namespace ElectroCircuitFriendRemake.ViewModels
{
    public class CreateComponentViewModel : IValidatableObject
    {
        [Required]
        [Display(Name="Component type")]
        public ComponentCategories ComponentCategory { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(2024)]
        public string Description { get; set; }
        [StringLength(2024)]
        [Display(Name="Optional description")]
        public string ExtraDescription { get; set; }
        [Display(Name="Component image", Prompt= "Enter URL or upload a image using the browse button")]
        public string ComponentImage { get; set; }
        public IFormFile ComponentImageFile { get; set; }
        [Display(Name="Pinout Image", Prompt = "Enter URL or upload a image using the browse button")]
        public string ComponentPinoutImage { get; set; }
        public IFormFile ComponentPinoutImageFile { get; set; }
        [Display(Name="Datasheet", Prompt = "Enter URL or upload a image using the browse button")]
        public string DataSheet { get; set; }
        public IFormFile DatasheetFile { get; set; }
        [Required]
        [DefaultValue(0)]
        [Display(Name="Components owned")]
        public int InStock { get; set; }
        [Required]
        [DefaultValue(0)]
        [Display(Name= "Components used")]
        public int Used { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Used > InStock)
            {
                yield return new ValidationResult(
                   "Components used cannot be higher then Components owned",
                    new[] { "InStock", "Used" });
            }
            if (DatasheetFile != null && DataSheet != DatasheetFile.FileName)
            {
                yield return new ValidationResult(
                    "Text input is not the same as the file upload.</br>Did you wanted to change it to something else?",
                    new[] {"DataSheet"});
            }
            if (ComponentImageFile != null && ComponentImage != ComponentImageFile.FileName)
            {
                yield return new ValidationResult(
                    "Text input is not the same as the file upload.</br>Did you wanted to change it to something else?",
                    new[] {"ComponentImage"});
            }
            if (ComponentPinoutImageFile != null && ComponentPinoutImage != ComponentPinoutImageFile.FileName)
            {
                yield return new ValidationResult(
                    "Text input is not the same as the file upload.</br>Did you wanted to change it to something else?",
                    new[] { "ComponentPinoutImage" });
            }
        }
    }
}
