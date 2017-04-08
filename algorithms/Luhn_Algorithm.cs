/*
LUHN Algortihm: 
	The Luhn algortihm or Luhn formula, also known as the "modulus 10" algortihm is a simple checksum formula
	used to validate a variety of identification numbers such as credit card numbers, IMEI numbers, 
	National Provier Identifier Numbers etc.
	WIKI : https://en.wikipedia.org/wiki/Luhn_algorithm
	
WHAT IT DOES :
	This algorithm is used for generating numbers with certain length, it could be for credit card numbers or IMEI numbers or for any other number.
	The LUHN algorithm works by taking a series of digits and applying a checksum. The digits are valid if the checksum modulo 10 is equal to 0.

WHY is it REQUIRED: 
	Many times user inputed entries can be done with typo mistakes, or scrambling of numbers like 1 to 7, 23 to 32 etc.
	So how to validate whether entered number is correct or not? 
	And the easiest solution is to use Luhn formula, though it has some drawback but still it serves the basic purpose. 
*/

/* This LUHN utility class provides a robust and complete means of checking the validity of a credit card number using LINQ. 
   It uses several maths tricks and properties of the LUHN formula to reduce the LINQ statement down to two steps*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public static class LuhnUtility
{
   public static bool IsCardNumberValid(string cardNumber, bool allowSpaces = false)
   {
      if (allowSpaces)
      {
         cardNumber = cardNumber.Replace(" ", "");
      }

      if (cardNumber.Any(c => !Char.IsDigit(c)))
      {
         return false;
      }

      int checksum = cardNumber
         .Select((c, i) => (c - '0') << ((cardNumber.Length - i - 1) & 1))
         .Sum(n => n > 9 ? n - 9 : n);

      return (checksum % 10) == 0 && checksum > 0;
   }
}


/* BUSINESS CASE:
   The LUHN algorithm is a popular way to validate credit card numbers. 
   I’ve used it many times while developing e-commerce applications to check that a user has entered their credit card number correctly. 
   By using the LUHN algorithm to verify a card number, you can let a customer know their card number is invalid before taking payment through a gateway. 
   After all, it’s a better user experience if they don’t have to wait for the server to try and authorize their card through a payment gateway with incorrect details that could have been detected 

   Now that we have created the basic function to implement LUHN utility, its pretty easy to create a validator control for ASP.NET Web Forms:
*/   

public sealed class LuhnValidator : BaseValidator
{
   public bool AllowSpaces
   {
      get
      {
         return (bool)(ViewState["AllowSpaces"] ?? false);
      }
      set
      {
         ViewState["AllowSpaces"] = value;
      }
   }

   public bool AllowEmpty
   {
      get
      {
         return (bool)(ViewState["AllowEmpty"] ?? false);
      }
      set
      {
         ViewState["AllowEmpty"] = value;
      }
   }

   protected override bool EvaluateIsValid()
   {
      string value = GetControlValidationValue(ControlToValidate);

      if (AllowEmpty && String.IsNullOrEmpty(value))
      {
         return true;
      }

      return LuhnUtility.IsCardNumberValid(value, AllowSpaces);
   }

   protected override void OnPreRender(EventArgs e)
   {
      base.OnPreRender(e);

      if (RenderUplevel)
      {
         RegisterProperty("evaluationfunction", "luhnValidatorEvaluateIsValid");

         if (AllowSpaces)
         {
            RegisterProperty("evaluationspaces", "true");
         }

         if (AllowEmpty)
         {
            RegisterProperty("evaluationempty", "true");
         }

         RegisterScript("~/LuhnUtility.js");
         RegisterScript("~/LuhnValidator.js");
      }
   }

   private void RegisterProperty(string name, string value)
   {
      ScriptManager.RegisterExpandoAttribute(this, ClientID, name, value, true);
   }

   private void RegisterScript(string url)
   {
      ScriptManager.RegisterClientScriptInclude(this, GetType(), url, ResolveUrl(url));
   }
}

/* To perform model validation in MVC requires a custom validation attribute: */
public class LuhnAttribute : ValidationAttribute, IClientValidatable
{
   public bool AllowSpaces { get; set; }
   public bool AllowEmpty { get; set; }

   public override bool IsValid(object value)
   {
      string cardNumber = (string)value;

      if (String.IsNullOrEmpty(cardNumber))
      {
         return AllowEmpty;
      }

      return LuhnUtility.IsCardNumberValid(cardNumber, AllowSpaces);
   }

   public IEnumerable<ModelClientValidationRule>
      GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
   {
      yield return new LuhnRule(ErrorMessage, AllowSpaces, AllowEmpty);
   }

   class LuhnRule : ModelClientValidationRule
   {
      public LuhnRule(string errorMessage, bool allowSpaces, bool allowEmpty)
      {
         ErrorMessage = errorMessage;
         ValidationType = "luhn";

         ValidationParameters["allowspaces"] = allowSpaces;
         ValidationParameters["allowempty"] = allowEmpty;
      }
   }
}

/* This attribute can then be placed on a midel like this for instant validation */
public class PaymentModel
{
   [Luhn(ErrorMessage="Please enter a valid card number", AllowSpaces=false)]
   public string CardNumber { get; set; }
}

/* An example of the accompanying Razor would be like this : */
@using (Html.BeginForm())
{
   <div>
      @Html.TextBoxFor(m => m.CardNumber)
      @Html.ValidationMessageFor(m => m.CardNumber)

      <input type="submit" />
   </div>
}