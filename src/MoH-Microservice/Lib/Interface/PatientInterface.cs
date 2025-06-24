using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using static MoH_Microservice.Misc.AppReportModel;

namespace MoH_Microservice.Lib.Interface
{
    public interface PatientInterface
    {
        public Task<PatientReg> addPatient(AppUser user,PatientReg patient);
        public Task<PatientUpdate> modifyPatient(AppUser user, PatientUpdate patient);
        public Task<bool> modifyAccedentPatient(AppUser user, PatientAccedentsReg patient);
        public Task<ProvidersMapUsers> addPatientCBHI(AppUser user, ProvidersMapReg providers);
        public Task<PatientAccedent> addPatientAccedent(AppUser user, PatientAccedentsReg patient);
        public Task<IEnumerable<PatientViewDTO>> getAllPatient();
        public Task<IEnumerable<PatientViewDTO>> getOnePatient(string cardnumber);
        public Task<IEnumerable<PatientViewDTO>> findPatient(PatientView searchKey);
        public Task<IEnumerable<PatientViewDTO>> getAllPatientCBHI();
        public AccidentGroupModelDTO[] getAllPatientAccedent(PatientView patient);
        public Task<IEnumerable<PatientViewDTO>> getOnePatientCBHI(string cardnumber);
        public Task<IEnumerable<PatientViewDTO>> findPatientCBHI(PatientView searchKey);
        public Task<bool> removePatient(string cardnumber);


    }
}
