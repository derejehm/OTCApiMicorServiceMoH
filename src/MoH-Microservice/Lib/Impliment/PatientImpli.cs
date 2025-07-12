using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MoH_Microservice.Data;
using MoH_Microservice.Lib.Interface;
using MoH_Microservice.Misc;
using MoH_Microservice.Models.Database;
using MoH_Microservice.Models.Form;
using MoH_Microservice.Query;
using static MoH_Microservice.Misc.AppReportModel;

namespace MoH_Microservice.Lib.Impliment
{
    public class PatientImpli : RequestImpli, PatientInterface
    {
        private readonly AppDbContext _dbContext;
        private readonly AppQuery _appQuery;

        public PatientImpli(AppDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
            this._appQuery = new AppQuery(this._dbContext);
            
        }
        public  async Task<PatientReg> addPatient(AppUser user,PatientReg patient)
        {
            long nextSequenceValue;
            using (var command = this._dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR patient_card_number";
                this._dbContext.Database.OpenConnection();

                var result = await command.ExecuteScalarAsync();
                nextSequenceValue = Convert.ToInt64(result);
            }

            var nextUnique = patient.PatientCardNumber.IsNullOrEmpty() ? $"{nextSequenceValue}".PadLeft(8, '0') : patient.PatientCardNumber;

            Patient _patient = new Patient
            {
                MRN = nextUnique,
                firstName = patient.PatientFirstName,
                middleName = patient.PatientMiddleName,
                lastName = patient?.PatientLastName,
                motherName = patient?.PatientMotherName,
                PatientDOB = Convert.ToDateTime(patient.PatientDOB),// DateTime.ParseExact(patient.PatientDOB.ToString(), "yyyy-MMM-dd  HH:mm:ss 'GMT'K", null),
                gender = patient?.PatientGender,
                religion = patient?.PatientReligion,
                placeofbirth = patient?.PatientPlaceofbirth,
                multiplebirth = patient?.Multiplebirth,
                appointment = patient?.Appointment,
                phonenumber = patient?.PatientPhoneNumber,
                iscreadituser = patient?.iscreadituser,
                iscbhiuser = patient?.iscbhiuser,
                EmployementID = patient.iscreadituser==true ? patient?.PatientEmployementID : null,
                occupation = patient?.PatientOccupation,
                department = patient?.Department,
                educationlevel = patient?.PatientEducationlevel,
                maritalstatus = patient?.PatientMaritalstatus,
                spouseFirstName = patient?.PatientSpouseFirstName,
                spouselastName = patient?.PatientSpouselastName,
                CreatedBy = user.UserName,
                type = patient?.PatientType,
                visitDate = Convert.ToDateTime(patient.PatientVisitingDate),
                CreatedOn = DateTime.Now.Date,
                UpdatedBy = null,
                UpdatedOn = null
            };
            PatientAddress patientAddress = new PatientAddress
            {
                MRN = nextUnique,
                Region = patient?.PatientRegion,
                Woreda = patient?.PatientWoreda,
                Kebele = patient?.PatientKebele,
                HouseNo = patient?.PatientHouseNo,
                Mobile = patient?.PatientPhoneNumber,
                AddressDetail = patient?.PatientAddressDetail,
                Phone = patient?.PatientPhone,
                CreatedBy = _patient.CreatedBy,
                CreatedOn = DateTime.Now.Date,
            };

            PatientAddress patientKinAddress = new PatientAddress
            {
                REFMRN = nextUnique,
                Region = patient?.PatientKinRegion,
                Woreda = patient?.PatientKinWoreda,
                Kebele = patient?.PatientKinKebele,
                HouseNo = patient?.PatientKinHouseNo,
                Mobile = patient?.PatientKinMobile,
                AddressDetail = patient?.PatientKinAddressDetail,
                Phone = patient?.PatientKinPhone,
                isNextOfKin = 1,
                CreatedBy = _patient.CreatedBy,
                CreatedOn = DateTime.Now.Date,
            };

            if (patient.iscbhiuser == true)
            {
                ProvidersMapUsers provider = new ProvidersMapUsers
                {
                    MRN = _patient.MRN,
                    provider = patient.Woreda,
                    Kebele = patient.Kebele,
                    Goth = patient.Goth,
                    IDNo = patient.IDNo,
                    letterNo = patient.letterNo,
                    Examination = patient.Examination,
                    service = "CBHI",
                    CreatedBy = _patient.CreatedBy,
                    CreatedOn = DateTime.Now,
                    ReferalNo = patient.ReferalNo,

                };
                var la = await this._dbContext.ProvidersMapPatient
                .GroupBy(p => p.MRN).Select(s => new { latestRecord = s.Max(s => s.Id) }).ToArrayAsync();

                await this._dbContext.Patients.Where(w => w.MRN == _patient.MRN).ExecuteUpdateAsync(u => u.SetProperty(p => p.cbhiId, la.FirstOrDefault().latestRecord));
                // if the user is a CBHI User
                await this._dbContext.AddAsync<ProvidersMapUsers>(provider);
                await this._dbContext.SaveChangesAsync();
            }
            else
            {
                patient.Woreda = patient.Kebele = patient.IDNo = patient.letterNo = patient.Examination = patient.Woreda = patient.ReferalNo = null;
            }
            await this._dbContext.AddAsync(_patient);
            await this._dbContext.AddAsync(patientAddress);
            await this._dbContext.AddAsync(patientKinAddress);
            await this._dbContext.SaveChangesAsync();
            patient.PatientCardNumber = nextUnique;

            return patient;
        }
        public  async Task<PatientUpdate> modifyPatient(AppUser user, PatientUpdate patient)
        {
            var Patient = await this._dbContext
                           .Patients
                           .Where(e => e.MRN == patient.PatientCardNumber)
                           .ExecuteUpdateAsync(e =>
                            e.SetProperty(p => p.firstName, patient.PatientFirstName)
                            .SetProperty(p => p.middleName, patient.PatientMiddleName)
                            .SetProperty(p => p.lastName, patient.PatientLastName)
                            .SetProperty(p => p.motherName, patient.PatientMotherName)
                            .SetProperty(p => p.gender, patient.PatientGender)
                            .SetProperty(p => p.religion, patient.PatientReligion)
                            .SetProperty(p => p.placeofbirth, patient.PatientPlaceofbirth)
                            .SetProperty(p => p.multiplebirth, patient.Multiplebirth)
                            .SetProperty(p => p.appointment, patient.Appointment)
                            .SetProperty(p => p.phonenumber, patient.PatientPhoneNumber)
                            .SetProperty(p => p.iscreadituser, patient.iscreadituser)
                            .SetProperty(p => p.iscbhiuser, patient.iscbhiuser)
                            .SetProperty(p => p.EmployementID, patient.iscreadituser == true ? patient.PatientEmployementID : null)
                            .SetProperty(p => p.occupation, patient.PatientOccupation)
                            .SetProperty(p => p.department, patient.Department)
                            .SetProperty(p => p.educationlevel, patient.PatientEducationlevel)
                            .SetProperty(p => p.maritalstatus, patient.PatientMaritalstatus)
                            .SetProperty(p => p.spouseFirstName, patient.PatientSpouseFirstName)
                            .SetProperty(p => p.spouselastName, patient.PatientSpouselastName)
                            .SetProperty(p => p.UpdatedBy, user.UserName)
                            .SetProperty(p => p.UpdatedOn, DateTime.Now.Date)
                            .SetProperty(p => p.PatientDOB, Convert.ToDateTime(patient.PatientDOB))
                            .SetProperty(p => p.visitDate, Convert.ToDateTime(patient.PatientVisitingDate))
                           );
            var PatientAddress = await this._dbContext
                       .PatientAddress
                       .Where(e => e.MRN == patient.PatientCardNumber)
                       .ExecuteUpdateAsync(e =>
                       e.SetProperty(e => e.Region, patient.PatientRegion)
                        .SetProperty(e => e.Woreda, patient.PatientWoreda)
                        .SetProperty(e => e.Kebele, patient.PatientKebele)
                        .SetProperty(e => e.AddressDetail, patient.PatientAddressDetail)
                        .SetProperty(e => e.HouseNo, patient.PatientHouseNo)
                        .SetProperty(e => e.Phone, patient.PatientPhone)
                        .SetProperty(p => p.UpdatedBy, user.UserName)
                        .SetProperty(p => p.UpdatedOn, DateTime.Now.Date)
                       );

            var PatientKinAddress = await this._dbContext
                       .PatientAddress
                       .Where(e => e.REFMRN == patient.PatientCardNumber && e.isNextOfKin == 1)
                       .ExecuteUpdateAsync(e =>
                       e.SetProperty(e => e.Region, patient.PatientKinRegion)
                        .SetProperty(e => e.Woreda, patient.PatientKinWoreda)
                        .SetProperty(e => e.Kebele, patient.PatientKinKebele)
                        .SetProperty(e => e.AddressDetail, patient.PatientKinAddressDetail)
                        .SetProperty(e => e.HouseNo, patient.PatientKinHouseNo)
                        .SetProperty(e => e.Phone, patient.PatientKinPhone)
                        .SetProperty(e => e.Mobile, patient.PatientKinMobile)
                        .SetProperty(p => p.UpdatedBy, user.UserName)
                        .SetProperty(p => p.UpdatedOn, DateTime.Now.Date)
                       );
            if (patient.iscbhiuser == true && patient.iscbhiuserUpdated == true)
            {
                // if the user is a CBHI User
                ProvidersMapUsers provider = new ProvidersMapUsers
                {
                    MRN = patient.PatientCardNumber,
                    provider = patient.Woreda,
                    Kebele = patient.Kebele,
                    Goth = patient.Goth,
                    IDNo = patient.IDNo,
                    letterNo = patient.letterNo,
                    Examination = patient.Examination,
                    service = "CBHI",
                    CreatedBy = user.UserName,
                    CreatedOn = DateTime.Now,
                    ReferalNo = patient.ReferalNo,
                };
                var la = await this._dbContext.ProvidersMapPatient
                .GroupBy(p => p.MRN).Select(s => new { latestRecord = s.Max(s => s.Id) }).ToArrayAsync();

                await this._dbContext.Patients.Where(w => w.MRN == provider.MRN).ExecuteUpdateAsync(u => u.SetProperty(p => p.cbhiId, la.FirstOrDefault().latestRecord));
                await this._dbContext.AddAsync<ProvidersMapUsers>(provider);
                await this._dbContext.SaveChangesAsync();
            }
            else
            {
                patient.Woreda = null;
                patient.Kebele = null;
                patient.Goth = null;
                patient.IDNo = null;
                patient.letterNo = null;
                patient.Examination = null;
                patient.Woreda = null;
                patient.ReferalNo = null;
            }
            await this._dbContext.SaveChangesAsync();

            return patient;
        }
        public async Task<ProvidersMapUsers> addPatientCBHI(AppUser user, ProvidersMapReg providers)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == providers.CardNumber).ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
                throw new Exception("PATIENT IS NOT REGISTEED / ታካሚው አልተመዘገበም !");

            ProvidersMapUsers provider = new ProvidersMapUsers
            {
                MRN = providers.CardNumber,
                provider = providers.provider,
                Kebele = providers.Kebele,
                Goth = providers.Goth,
                IDNo = providers.IDNo,
                letterNo = providers.letterNo,
                Examination = providers.Examination,
                service = providers.service,
                CreatedBy = user.UserName,
                CreatedOn = DateTime.Now,
                ReferalNo = providers.ReferalNo,
                ExpDate = Convert.ToDateTime(providers.ExpDate.ToString()),
            };
            await this._dbContext.AddAsync(provider);
            await this._dbContext.SaveChangesAsync();
            var la = await this._dbContext.ProvidersMapPatient
                .GroupBy(p => p.MRN).Select(s => new { latestRecord = s.Max(s => s.Id) }).ToArrayAsync();
            var id = la?.FirstOrDefault()?.latestRecord;
            await this._dbContext.Patients.Where(w => w.MRN == provider.MRN).ExecuteUpdateAsync(u => u.SetProperty(p => p.cbhiId, id).SetProperty(p=>p.iscbhiuser,true));            
            return provider;
        }
        public async Task<PatientAccedent> addPatientAccedent(AppUser user, PatientAccedentsReg patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }

            PatientAccedent accedents = new PatientAccedent
            {
                MRN = patient.PatientCardNumber,
                accedentAddress = patient.accAddress,
                accedentDate = Convert.ToDateTime(patient.accDate),
                policeName = patient.policeName,
                policePhone = patient.policePhone,
                plateNumber = patient.plateNumber,
                certificate = patient.certificate,
                CreatedOn = DateTime.Now,
                CreatedBy = user.UserName
            };
            await this._dbContext.AddAsync(accedents);
            await this._dbContext.SaveChangesAsync();

            return accedents;
        }

        public async Task<bool> modifyAccedentPatient(AppUser user, PatientAccedentsReg patient)
        {
            var doesPatientExisit = await this._dbContext.Patients.Where(e => e.MRN == patient.PatientCardNumber)
                    .AsNoTracking().ToArrayAsync();
            if (doesPatientExisit.Length <= 0)
            {
                throw new Exception("PATIENT DOES NOT EXIST.");
            }

            var accedents = await this._dbContext.PatientAccedents
                .Where(w => w.id == patient.id)
                .ExecuteUpdateAsync(u => u
                 .SetProperty(p => p.MRN, patient.PatientCardNumber)
                 .SetProperty(p => p.accedentAddress, patient.accAddress)
                 .SetProperty(p => p.accedentDate, Convert.ToDateTime(patient.accDate))
                 .SetProperty(p => p.policeName, patient.policeName)
                 .SetProperty(p => p.policePhone, patient.policePhone)
                 .SetProperty(p => p.plateNumber, patient.plateNumber)
                 .SetProperty(p => p.certificate, patient.certificate)
                 .SetProperty(p => p.UpdatedOn, DateTime.Now)
                 .SetProperty(p => p.UpdatedBy, user.UserName)
                );

            if (accedents > 0)
            {
                await this._dbContext.SaveChangesAsync();
                return true;
            }
            else
                throw new Exception("COULDN'T FIND THE ACCEDENT REGISTRATION.");
        }
        public async Task<IEnumerable<PatientViewDTO>> findPatient(PatientView patient)
        {

            var patientInfo = await this._appQuery.patientJoin();
            IEnumerable<PatientViewDTO> SearchResult = patientInfo;

            if (patient.PatientLastName.IsNullOrEmpty() &
                patient.PatientMiddleName.IsNullOrEmpty() &
                patient.PatientFirstName.IsNullOrEmpty() &
                patient.PatientCardNumber.IsNullOrEmpty() &
                patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult.OrderByDescending(o => o.Recoredid).Take(1000).ToList();
            }

            if (!patient.PatientCardNumber.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientCardNumber.ToLower().Contains(patient.PatientCardNumber.ToLower()));
            }
            if (!patient.PatientFirstName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientFirstName.ToLower().Contains(patient.PatientFirstName.ToLower()));
            }
            if (!patient.PatientMiddleName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientMiddleName.ToLower().Contains(patient.PatientMiddleName.ToLower()));
            }
            if (!patient.PatientLastName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientLastName.ToLower().Contains(patient.PatientLastName.ToLower()));
            }
            if (!patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientPhone.Contains(patient.PatientPhone) || e.PatientPhoneNumber.Contains(patient.PatientPhone));
            }

            if (patientInfo == null)
                throw new Exception("PATIENT DOES NOT EXIST");

            return SearchResult;
        }
        public async Task<int> findPatientCount()
        {
            var patientInfo = await this._appQuery.PatientQuery().ToArrayAsync();
            return patientInfo.Count();
        }
        public async Task<IEnumerable<PatientViewDTO>> findPatientCBHI(PatientView patient)
        {
            var patientInfo = await this._appQuery.patientJoin();
            IEnumerable<PatientViewDTO> SearchResult = patientInfo.Where(w=>w.iscbhiuser==true);

            if (patient.PatientLastName.IsNullOrEmpty() &
                patient.PatientMiddleName.IsNullOrEmpty() &
                patient.PatientFirstName.IsNullOrEmpty() &
                patient.PatientCardNumber.IsNullOrEmpty() &
                patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult.OrderByDescending(o => o.Recoredid).Take(1000).ToList();
            }

            if (!patient.PatientCardNumber.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientCardNumber.ToLower().Contains(patient.PatientCardNumber.ToLower()));
            }
            if (!patient.PatientFirstName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientFirstName.ToLower().Contains(patient.PatientFirstName.ToLower()));
            }
            if (!patient.PatientMiddleName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientMiddleName.ToLower().Contains(patient.PatientMiddleName.ToLower()));
            }
            if (!patient.PatientLastName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientLastName.ToLower().Contains(patient.PatientLastName.ToLower()));
            }
            if (!patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientPhone.Contains(patient.PatientPhone) || e.PatientPhoneNumber.Contains(patient.PatientPhone));
            }

            if (patientInfo == null)
                throw new Exception("PATIENT DOES NOT EXIST");

            return SearchResult;
        }
        public async Task<IEnumerable<PatientViewDTO>> getAllPatient()
        {
            var patientInfo = await this._appQuery.PatientQuery().ToArrayAsync();
            IEnumerable<PatientViewDTO> SearchResult = patientInfo;
            return SearchResult;
        }
        public async Task<IEnumerable<PatientViewDTO>> getAllPatientCBHI()
        {
            var patientInfo = await this._appQuery.PatientQueryCBHI().ToArrayAsync();
            return patientInfo.Where(w => w.iscbhiuser == true);
        }
        public AccidentGroupModelDTO[] getAllPatientAccedent(PatientView patient)
        {
            var accedents = this._appQuery.PatientQuery();
            IEnumerable<PatientViewDTO> SearchResult = accedents.Where(w => w.AccedentRecId != null);

            if (patient.PatientLastName.IsNullOrEmpty() &
                patient.PatientMiddleName.IsNullOrEmpty() &
                patient.PatientFirstName.IsNullOrEmpty() &
                patient.PatientCardNumber.IsNullOrEmpty() &
                patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult.OrderByDescending(o => o.Recoredid).Take(1000).ToList();
            }

            if (!patient.PatientCardNumber.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientCardNumber.ToLower().Contains(patient.PatientCardNumber.ToLower()));
            }
            if (!patient.PatientFirstName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientFirstName.ToLower().Contains(patient.PatientFirstName.ToLower()));
            }
            if (!patient.PatientMiddleName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientMiddleName.ToLower().Contains(patient.PatientMiddleName.ToLower()));
            }
            if (!patient.PatientLastName.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientLastName.ToLower().Contains(patient.PatientLastName.ToLower()));
            }
            if (!patient.PatientPhone.IsNullOrEmpty())
            {
                SearchResult = SearchResult.Where(e => e.PatientPhone.Contains(patient.PatientPhone));
            }
            var Output = SearchResult.GroupBy(accedents => new
            {
                accedents.AccedentRecId,
                accedents.AccedentDate,
                accedents.PatientCardNumber,
                accedents.PatientFirstName,
                accedents.PatientMiddleName,
                accedents.PatientLastName,
                accedents.PatientDOB,
                accedents.PatientAge,
                accedents.AcceedentAddress,
                accedents.PlateNumber,
                accedents.CarCertificate,
                accedents.PoliceName,
                accedents.PolicePhone,
            }).Select(accedents =>
                        new AccidentGroupModelDTO
                        {
                            AccedentRecId= accedents?.FirstOrDefault()?.AccedentRecId,
                            AccedentDate =accedents?.FirstOrDefault()?.AccedentDate,
                            PatientCardNumber = accedents?.FirstOrDefault()?.PatientCardNumber,
                            PatientFirstName = accedents?.FirstOrDefault()?.PatientFirstName,
                            PatientMiddleName = accedents?.FirstOrDefault()?.PatientMiddleName,
                            PatientLastName = accedents?.FirstOrDefault()?.PatientLastName,
                            PatientDOB = accedents?.FirstOrDefault()?.PatientDOB,
                            PatientAge = accedents?.FirstOrDefault()?.PatientAge,
                            AcceedentAddress = accedents?.FirstOrDefault()?.AcceedentAddress,
                            PlateNumber = accedents?.FirstOrDefault()?.PlateNumber,
                            CarCertificate = accedents?.FirstOrDefault()?.CarCertificate,
                            PoliceName = accedents?.FirstOrDefault()?.PoliceName,
                            PolicePhone = accedents?.FirstOrDefault()?.PolicePhone,
                        }).ToArray();

            return Output;
        }
        public async Task<IEnumerable<PatientViewDTO>> getOnePatient(string cardnumber)
        {
            var patientInfo = await this._appQuery.patientJoin();
            return patientInfo.Where(w =>  w.PatientCardNumber == cardnumber);
        }
        public async Task<IEnumerable<PatientViewDTO>> getOnePatientCBHI(string cardnumber)
        {

            var patientInfo = await this._appQuery.patientJoin();
            return patientInfo.Where(w=>w.iscbhiuser==true && w.PatientCardNumber==cardnumber);
        }
        public Task<bool> removePatient(string cardnumber)
        {
            throw new NotImplementedException();
        }
 
    }
}
