using System;
using System.Xml.Serialization;
using System.Globalization;

public enum RoundingMode : int {
	IEEE754Section4 = 1,
	AlwaysRoundUp,
	AlwaysRoundDown,
	RoundPoint5AndHigher
}

namespace Cuyahoga.Modules.ECommerce.Util {

	/// <summary>
	/// Summary description for Class.
	/// </summary>
	/// <remarks>
	/// Default rounding in this class follows IEEE Standard 754, section 4.
	/// This kind of rounding is sometimes called rounding to nearest, or banker's rounding.
	/// </remarks>
	public class Money : IComparable {

		public event OnChangeHandler OnChange;
		public delegate void OnChangeHandler(Money money);

		private Currency _currency = new Currency();
		private long _value = 0;
		private RoundingMode _roundingMode = RoundingMode.IEEE754Section4;

		public Money() {
            Currency = new Currency(System.Threading.Thread.CurrentThread.CurrentCulture);
		}

        public Money(decimal amount) : this() {
            Amount = amount;
        }

		#region Constructors
		public Money(Money money) {
			Currency = new Currency(money.Currency.CultureInfo);
			_value = money.LongValue;
		}

        public Money(Money money, decimal amount) : this(money) {
            Amount = amount;
        }
        
		public Money(string currencyCode, decimal amount) {
            _currency = new Currency();
            if (currencyCode != null) {//sometimes a null currencyCode is passed. This might be a bug somewhere
                _currency.CurrencyCode = currencyCode;
            }
			Amount = amount;
		}

		public Money(CultureInfo info, decimal amount) {
			_currency = new Currency(info);
			Amount = amount;
		}

        public Money(Currency currency, decimal amount) {
			_currency = currency;
			Amount = amount;
		}
		#endregion

		#region Properties
		public Currency Currency {
			get {
				return _currency;
			}
			set {
				_currency = value;
				RaiseChangeEvent();
			}
		}

        /// <summary>
        /// The numeric, floating point, value of this object
        /// </summary>
        /// <remarks>
        /// Where possible, operations should be performed on the LongValue
        /// </remarks>
        public decimal Amount {
            get {
                return ((decimal) LongValue) / ScalingFactor;
            }
            set {
                //Use IEEE rounding to avoid floating point rounding errors - might introduce a slight bug
                decimal initialRound = Math.Round((value * Convert.ToDecimal(ScalingFactor)), 1);

                //Get single fraction of penny/whatever
                decimal fraction = initialRound % 1;

                if (fraction == 0) {
                    LongValue = (long)initialRound;
                } else {
                    switch (RoundingMode) {
                        case RoundingMode.AlwaysRoundDown:
                            LongValue = RoundDown(value);
                            break;
                        case RoundingMode.AlwaysRoundUp:
                            LongValue = RoundUp(value);
                            break;
                        case RoundingMode.RoundPoint5AndHigher:
                            if (fraction >= 0.5M) {
                                LongValue = RoundUp(value);
                            } else {
                                LongValue = RoundDown(value);
                            }
                            break;
                        case RoundingMode.IEEE754Section4:
                        default:
                            LongValue = (long)(Math.Round(value * ScalingFactor, 0));
                            break;
                    }
                }
            }
        }

        [XmlIgnore]
        public RoundingMode RoundingMode {
            get {
                return _roundingMode;
            }
            set {
                _roundingMode = value;
            }
        }

        /// <summary>
        /// The raw value of this object.
        /// For pounds and dollars this will be the amount multiplied by 100
        /// </summary>
        [XmlIgnore]
        public long LongValue {
            get {
                return _value;
            }
            set {
                _value = value;
                RaiseChangeEvent();
            }
        }

        public string CurrencyCode {
            get {
                return Currency.CurrencyCode;
            }
        }

		public override string ToString() {
            return ToString(true);
		}

        public string ToString(bool useCurrencySymbol) {

            if (useCurrencySymbol && Currency.IsCultureCurrency) {
                return Amount.ToString("C", Currency.CultureInfo);
            } else {
                return Amount.ToString("f" + Currency.FractionDigits, Currency.CultureInfo) + " " + CurrencyCode;
            }

            /*
            if (useCurrencySymbol) {
                return Amount.ToString("C", Currency);
            } else {
                return Amount.ToString("C", Currency);
            }
             */
        }

        public string ToPlainText() {
            return ToString(false);
        }
		#endregion

		#region operations to modify the current object
		public void Add(Money price) {
			if (price.Currency.CurrencyCode == Currency.CurrencyCode) {
				Amount += price.Amount;
			} else {
				throw new ArgumentException("Non-identical currency codes");
			}
		}

		public void Subtract(Money price) {
			if (price.Currency.CurrencyCode == Currency.CurrencyCode) {
				Amount -= price.Amount;
			} else {
				throw new ArgumentException("Non-identical currency codes");
			}
		}

		public void Multiply(decimal mult) {
			Amount *= mult;
		}

		public void Multiply(int mult) {
			Amount *= mult;
		}

		public void Divide(decimal div) {
			if (div != 0) {
				Amount = Amount / div;
			}
		}

		public void Divide(int div) {
			if (div != 0) {
				Amount = Amount / div;
			}
		}
		#endregion

		#region Operators
		public override bool Equals(Object money) {
			try {
				Money compare = (Money) money;
				if (compare.Currency.CurrencyCode == Currency.CurrencyCode) {
					return compare.LongValue == _value;
				} else {
					return false;
				}
			} catch {
				return false;
			}
		}

		public bool LessThan(Money money) {
			if (money.Currency.CurrencyCode == Currency.CurrencyCode) {
				return _value < money.LongValue;
			} else {
				throw new ArgumentException("Non-identical currency codes");
			}
		}
        
		public bool GreaterThan(Money money) {
			if (money.Currency.CurrencyCode == Currency.CurrencyCode) {
				return _value > money.LongValue;
			} else {
				throw new ArgumentException("Non-identical currency codes");
			}
		}

		public int CompareTo(Object money) {

			Money compare = (Money) money;
			if (compare.Currency.CurrencyCode == Currency.CurrencyCode) {
				if (GreaterThan(compare)) {
					return 1;
				}
				if (LessThan(compare)) {
					return -1;
				}
				return 0;
			} else {
				throw new ArgumentException("Non-identical currency codes");
			}
		}
        
		public override int GetHashCode () {
			return _value.GetHashCode();
		}
		#endregion

		/// <summary>
		/// Tells listeners that values have changed
		/// </summary>
		private void RaiseChangeEvent() {
			if (OnChange != null) {
				OnChange(this);
			}
		}

        private long RoundDown(decimal floatValue) {
            return Convert.ToInt64(Math.Floor(floatValue * ScalingFactor));
        }

        private long RoundUp(decimal floatValue) {
            return RoundDown(floatValue) + 1;
        }
		
		/// <summary>
		/// Used to convert long value to floating point value
		/// </summary>
		private int ScalingFactor {
			get {
				return (int) Math.Pow(10, Currency.FractionDigits);
			}
		}
	}
}