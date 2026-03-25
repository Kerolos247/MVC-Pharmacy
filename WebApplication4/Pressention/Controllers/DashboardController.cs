//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WebApplication4.Application.Dto.Dashboard;
//using WebApplication4.Application.IServices;
//using WebApplication4.Application.Services;

//namespace WebApplication4.Pressention.Controllers
//{
   
//    [Authorize(Roles = "Admin,Pharmacist")]
//    public class DashboardController : Controller
//    {
//        private readonly IMedicineService _medicineService;
//        private readonly IPatientService _patientService;
//        private readonly ISupplierService _supplierService;
//        private readonly IPrescriptionService _prescriptionService;
//        private readonly IFeedBackService _feedBackService;
//        public DashboardController(
//           ISupplierService supplierService,
//           IPatientService patientService,
//           IMedicineService medicineService,
//           IPrescriptionService prescriptionService,
//           IFeedBackService feedBackService)
//        {
//            _supplierService = supplierService;
//            _patientService = patientService;
//            _medicineService = medicineService;
//            _prescriptionService = prescriptionService;
//            _feedBackService = feedBackService;
//        }
//    }
//}
