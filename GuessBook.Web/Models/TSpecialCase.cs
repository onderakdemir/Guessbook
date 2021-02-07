using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;


public enum TSpecialCaseType { none = 0, info = 1, error = 2, warn = 3, };

    public class TException : Exception
    {
        public byte fMessageState;
        public TException(string message, byte message_state) : base(message)
        {
            fMessageState = message_state;
        }
    }

    public class TMResult
    {
        public TSpecialCaseType fSpecialCaseType;
        public string fMessage;
        public Exception fException;
 
    }

    public class TExcResult
    {
        public TExcResult()
        {

        }
        public TExcResult(Exception exception)
        {
            this.Exception = exception;
        }
        public Exception Exception;
        public object ResultObject;
    }

    public class TResult
    {
        public TResult()
        {

        }
        public TResult(bool hasError, string message)
        {
            this.HasError = hasError;
            this.Message = message;
        }

        public void mSaveToDB(string code)
        {
            //save this.Message
        }

        public void mSetUserError(string message)
        {
            this.HasError = true;
            this.Message = message;
            this.isUserError = true;
        }

        public void mSetDBError(string message)
        {
            this.HasError = true;
            this.Message = message;
            this.isUserError = false;
        }

        public bool isUserError;
        public bool HasError;
        public string Message;
        public object ResultObject;
        public string sensor;
        public string ip;
        public int userid;

    }


    public class TDataTableResult : TResult
    {
        public new DataTable ResultObject;
        public void SetException(Exception e)
        {
            this.HasError = true;
            this.Message = e.Message;
        }

    }

    public class TExResult<T>
    {
        public TExResult(T oresult, Exception e)
        {
            fResultObject = oresult;
            fException = e;
        }

        public T fResultObject;
        public Exception fException;
    }

    public class TResult<T>
    {
        public TResult()
        {

        }

        public TResult(bool hasError, string message, T resultObject)
        {
            this.HasError = hasError;
            this.Message = message;
            this.ResultObject = resultObject;
        }

        public bool HasError;
        public string Message;
        public T ResultObject;
        public Exception Exception;

        public TResult ToResult(bool HasException)
        {
            TResult res = new TResult();
            res.HasError = this.HasError;
            res.Message = this.Message;
            if (HasException)
                res.ResultObject = this.Exception;
            else
                res.ResultObject = this.ResultObject;

            return res;
        }
    }


    public class Person
    {

    }

    public class Customer
    {

        public IEnumerable getOrders()
        {
            return new List<string>(10);
        }
    }

    //---



    public class TSpecialCase
    {
        public bool fSuccess;
        public string fMessage;

        public TSpecialCase()
        {

        }

        public TSpecialCase(bool success, string message)
        {
            fSuccess = success;
            fMessage = message;
        }
    }

    public class TSpecialCase<T>
    {
        public bool fSuccess;
        public string fMessage;
        public T fResult;
    }

    public class DBExecResult : TSpecialCase
    {

    }

    public class DBListResult : TSpecialCase
    {
        public IEnumerable fResult;
        public DBListResult()
        {

        }

        public TFindResult<Person> GetPerson(int personId)
        {
            TFindResult<Person> find = new TFindResult<Person>(false, "Test", null);

            if (find.fFound)
                Console.WriteLine(find.fResult);
            else
                Console.WriteLine(find.fMessage);

            return find;
        }
    }

    public class TFindResult<T>
    {
        public string fMessage { get; set; }
        public bool fFound { get; set; }
        public T fResult { get; set; }

        public TFindResult()
        {

        }
        public TFindResult(bool found, string message, T result)
        {
            this.fFound = found;
            this.fResult = result;
        }
    }

    public class TProcessResult<T> : TSpecialCase
    {
        public T fItem;
    }
 
