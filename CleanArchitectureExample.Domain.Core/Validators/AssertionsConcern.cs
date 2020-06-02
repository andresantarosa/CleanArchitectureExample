using CleanArchitectureExample.Domain.Core.DomainNotification;
using MediatR;
using System;

namespace CleanArchitectureExample.Domain.Core.Validators
{

    public static class AssertionsConcern
    {
        public static bool HasNotifications()
        {
            return DomainNotificationsFacade.HasNotifications();
        }
        public static bool IsSatisfiedBy(params Func<bool>[] asserts)
        {
            var isSatisfied = true;

            foreach (var assert in asserts)
                if (!assert())
                    isSatisfied = false;

            return isSatisfied;
        }


        public static Func<bool> IsEquals(string value1, string value2, string message)
        {
            return delegate ()
            {
                if (!(value1 == value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsEquals(int value1, int value2, string message)
        {
            return delegate ()
            {
                if (!(value1 == value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsDateEquals(DateTime date1, DateTime date2, string message)
        {
            return delegate ()
            {
                if (!(date1.Date == date2.Date))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }



        public static Func<bool> IsLowerThan(int? value1, int value2, string message)
        {
            return delegate ()
            {
                if (!(value1 < value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }

        public static Func<bool> IsGreaterThan(int? value1, int value2, string message)
        {
            return delegate ()
            {
                if (!(value1 > value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }

        public static Func<bool> IsGreaterThanOrEquals(int? value1, int value2, string message)
        {
           return delegate ()
            {
                if (!(value1 >= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }

        public static Func<bool> IsLowerOrEquals(int? value1, int value2, string message)
        {
            return delegate ()
            {
                if (!(value1 <= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }



        public static Func<bool> IsLowerThan(decimal? value1, decimal value2, string message)
        {
            return delegate ()
            {
                if (!(value1 < value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsGreaterThan(decimal? value1, decimal value2, string message)
        {
            return delegate ()
            {
                if (!(value1 > value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsGreaterThanOrEquals(decimal? value1, decimal value2, string message)
        {
            return delegate ()
            {
                if (!(value1 >= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsLowerOrEquals(decimal? value1, decimal value2, string message)
        {
            return delegate ()
            {
                if (!(value1 <= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }




        public static Func<bool> IsLowerThan(DateTime value1, DateTime value2, string message)
        {
            return delegate ()
            {
                if (!(value1 < value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsGreaterThan(DateTime value1, DateTime value2, string message)
        {
            return delegate ()
            {
                if (!(value1 > value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsGreaterThanOrEquals(DateTime value1, DateTime value2, string message)
        {
            return delegate ()
            {
                if (!(value1 >= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> IsLowerOrEquals(DateTime value1, DateTime value2, string message)
        {
            return delegate ()
            {
                if (!(value1 <= value2))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };

        }




        public static Func<bool> IsStringNotNullOrWhiteSpace(string value, string message)
        {
            return delegate ()
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> HasMinimumLength(string value, int minLength, string message)
        {
            return delegate ()
            {
                if (value.Length < minLength)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }

        public static Func<bool> HasLengthEquals(string value, int length, string message)
        {
           return delegate ()
            {
                if (value?.Length != length)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }



        public static Func<bool> IsGuidNotEmpty(Guid guid, string message)
        {
            return delegate ()
            {
                if (Guid.Empty == guid)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }
        public static Func<bool> IsGuidNotNull(Guid guid, string message)
        {
            return delegate ()
            {
                if (guid == null)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }



        public static Func<bool> ValidateGuidFromString(string guid, string stringIsEmptyMessage, string guidIsEmptyMessage, string guidIsInvalidMessage)
        {

            return delegate ()
            {
                var isValid = false;
                isValid = IsSatisfiedBy(IsStringNotNullOrWhiteSpace(guid, stringIsEmptyMessage));

                if (!isValid)
                    return false;

                Guid.TryParse(guid, out Guid parsed);

                isValid = IsSatisfiedBy(IsGuidNotNull(parsed, guidIsInvalidMessage), IsGuidNotEmpty(parsed, guidIsEmptyMessage));
                return isValid;
            };

        }
        public static Func<bool> IsNotNull(object obj, string message)
        {
            return delegate ()
            {
                if (obj == null)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }



        public static Func<bool> IsNull(object obj, string message)
        {
            return delegate ()
            {
                if (obj != null)
                {
                    DomainNotificationsFacade.AddNotification(message);
                    return false;
                }
                return true;
            };
        }
    }
}
