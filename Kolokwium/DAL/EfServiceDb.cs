using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolokwium.DAL
{
    class EfServiceDb
    {
        private static EmployeeDb context = new EmployeeDb();

        internal static ObservableCollection<EMP> LoadEMPsDataFromDb()
        {
            var res = context.EMPs.ToList();

            ObservableCollection<EMP> EMPList = new ObservableCollection<EMP>(res);
            return EMPList;
        }

        internal static ObservableCollection<DEPT> LoadDEPTsDataFromDb()
        {
            var res = context.DEPTs.ToList();

            ObservableCollection<DEPT> DEPTList = new ObservableCollection<DEPT>(res);
            return DEPTList;
        }

        internal static ObservableCollection<EMP> LoadEMPsWithStringInNameDataFromDb(String TextWithinEname)
        {
            var res = context.EMPs
                .Where(emp => emp.ENAME.Contains(TextWithinEname))
                .ToList();

            ObservableCollection<EMP> EMPList = new ObservableCollection<EMP>(res);
            return EMPList;
        }

        internal static bool addEmp(EMP NewEmp)
        {
            try
            {
                int CurrentMaxId = context.EMPs.Max(emp => emp.EMPNO);
                int NewEmpId = CurrentMaxId + 1;

                NewEmp.EMPNO = NewEmpId;

                context.EMPs.Add(NewEmp);
                context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

            
        }
    }
}
