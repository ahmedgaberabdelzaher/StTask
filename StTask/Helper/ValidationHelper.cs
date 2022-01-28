using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace StTask.Helper
{
    public interface IValidatable<T> : INotifyPropertyChanged
    {
        List<IValidationRule<T>> Validations { get; }

        List<string> Errors { get; set; }

        bool Validate();

        bool IsValid { get; set; }
    }
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; set; }
        bool Check(T value);
    }
    public class ValidatableObject<T> : INotifyPropertyChanged, IValidatable<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<IValidationRule<T>> Validations { get; } = new List<IValidationRule<T>>();
        List<string> errors = new List<string>();
        public List<string> Errors { get { return errors; } set { errors = value; OnPropertyChanged("Errors"); } }

        public bool CleanOnChange { get; set; } = true;

        T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                if (CleanOnChange)
                    IsValid = true;
                Validate();
            }
        }
        bool isvalid = false;
        public bool IsValid
        {
            get
            { return isvalid; }
            set
            {

                isvalid = value;
                OnPropertyChanged("IsValid");
            }
        }

        public virtual bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = Validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }
        public override string ToString()
        {
            return $"{Value}";
        }
    }

    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = $"{value }";
            return !string.IsNullOrWhiteSpace(str);
        }
    }

    public class IsValidEmailOrPhone<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                bool IsValid = false;

                if (value != null && value.ToString() != "")
                {
                     string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
                     string phoneRegex = "^[0-9]+$";

                    IsValid = (Regex.IsMatch(value.ToString(), phoneRegex));
                    if (IsValid)
                    {
                        if (value.ToString().Length < 11)
                        {
                            IsValid = false;
                        }
                    }
                    else
                    {
                        IsValid= Regex.IsMatch(value.ToString(),
 emailRegex,
 RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                    }

                }
                return IsValid;
            }
            catch
            {
                return false;
            }
        }
    }
    public class ValidatablePair<T> : IValidatable<ValidatablePair<T>>
    {
        public List<IValidationRule<ValidatablePair<T>>> Validations { get; } = new List<IValidationRule<ValidatablePair<T>>>();

        public bool IsValid { get; set; } = true;

        public List<string> Errors { get; set; } = new List<string>();

        public ValidatableObject<T> Item1 { get; set; } = new ValidatableObject<T>();
        ValidatableObject<T> _Item2 = new ValidatableObject<T>();
        public ValidatableObject<T> Item2
        {
            get
            {
                return _Item2;
            }
            set { _Item2 = value; Validate(); }
        }
        T _value;
        public T ItemValue
        {
            get { return _value; }
            set
            {
                _value = value; if (value != null)
                {
                    Validate();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Validate()
        {
            var item1IsValid = Item1.Validate();
            var item2IsValid = Item2.Validate();
            if (item1IsValid && item2IsValid)
            {
                Errors.Clear();

                IEnumerable<string> errors = Validations.Where(v => !v.Check(this))
                    .Select(v => v.ValidationMessage);

                Errors = errors.ToList();
                Item2.Errors = Errors;
                Item2.IsValid = !Errors.Any();
            }

            IsValid = !Item1.Errors.Any() && !Item2.Errors.Any();

            return this.IsValid;
        }
    }
    public class MatchPairValidationRule<T> : IValidationRule<ValidatablePair<T>>
    {
        public string ValidationMessage { get; set; }

        public bool Check(ValidatablePair<T> value)
        {
            return value.Item1.Value.Equals(value.Item2.Value);
        }
    }
    public class IsValidYear<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            try
            {
                bool IsValid = false;

                if (value != null && value.ToString() != "")
                {
                 string NoRegex = "^[0-9]+$";

                    IsValid = (Regex.IsMatch(value.ToString(), NoRegex));
                    if (IsValid)
                    {
                        if (value.ToString().Length !=4||int.Parse(value.ToString())>=2022)
                        {
                            IsValid = false;
                        }
                    }
                }
                else
                {
                    IsValid = true;
                }
                return IsValid;
            }
            catch
            {
                return false;
            }
        }
    }
}
