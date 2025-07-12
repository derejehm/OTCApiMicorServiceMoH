using MoH_Microservice.Data;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.DTO;
using MoH_Microservice.Models.Form;

namespace MoH_Microservice.Lib.Interface
{
    public interface RequestInterface
    {
        public Task<string> addLabRequest(AppUser user, PatientRequestedServicesReg PatientRequest);
        public  Task<string> modifyLabRequest();
        public Task<bool> cancelLabRequest(PatientRequestedServicesDelete patient);
        public Task<bool> completeLabRequest(AppUser user, PatientRequestedServicesViewOne patient);
        public Task<PatientReuestServicesDisplayDTO[]> getLabRequest(PatientRequestedServicesViewOne patient);
        public Task<PatientReuestServicesDisplayDTO[]> getLabRequest(AppUser user, PatientRequestedServicesViewOne patient);
        public Task<string> addNurseRequest(AppUser user, NurseRequestReg nurseRequest);
        public Task<string> modifyNurseRequest();
        public Task<bool> cancelNurseRequest(PatientRequestedServicesDelete patient);
        public Task<bool> completeNurseRequest(AppUser user, PatientRequestedServicesViewOne patient);
        public Task<NurseRequestDTO[]> getNurseRequest(NurseRequestGetOne patient);
        public Task<NurseRequestDTO[]> getNurseRequest(AppUser user);
        //---
        public Task<DoctorRequestDTO2[]> orderDoctorRequest(AppUser user, Models.Form.DoctorRequest doctor);
        public Task<bool> payDoctorRequest(AppUser user, long id, string groupid, string mrn);
        public Task<bool> pickDoctorRequest(AppUser user, long id);
        public Task<bool> proccessDoctorRequest(AppUser user, long id);
        public Task<bool> completeDoctorRequest(AppUser user, long id);
        public Task<bool> failedDoctorRequest(AppUser user, long id);
        public Task<DoctorRequestDTO2[]> getDoctorRequest(AppUser user);
        public Task<DoctorRequestDTO2[]> getDoctorRequestLab_v();
        public Task<DoctorRequestDTO2[]> getDoctorRequestPharma_v();
        public Task<DoctorRequestDTO2[]> getDoctorRequestCashier_v();


        /**
         * status
         *  0: ordered
         *  1: paid
         *  2: picked
         *  3: processed
         *  4: completed
         *  negetive status
         *  -1: equipment failed  
         *  -2: unkown
         * **/

    }
}
