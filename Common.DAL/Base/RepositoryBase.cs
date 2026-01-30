using Common.DAL.Exceptions;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DAL.Base
{
    public abstract class RepositoryBase
    {
        protected T ExuteDb<T>(Func<T> action, string errorMessage)
        {
            try
            {
                return action();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(errorMessage, ex);
            }
            catch(EmailException ex)
            {
                throw new EmailException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new ExternalServiceException(errorMessage, ex);
            }
        }

            protected void ExuteDb( Action action, string errorMessage)
        {
            try
            {
                action();
            }
            catch (SqlException ex)
            {
                throw new DatabaseException(errorMessage, ex);
            }
            catch(StudentIdException ex)
            {
                throw new StudentIdException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new ExternalServiceException(errorMessage, ex);
            }

        }

    }
}
