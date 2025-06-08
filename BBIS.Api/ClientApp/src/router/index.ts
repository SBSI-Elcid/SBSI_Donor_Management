import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import DashboardView from '../views/DashboardView.vue'
import Container from '../components/Container.vue';
import { StorageKeysEnum } from '@/common/StorageKeysEnum';
import BrowserStorageService from '@/services/BrowserStorageService';

Vue.use(VueRouter)

const routes: Array<RouteConfig> = [
    {
        path: '/',
        name: 'home',
        redirect: '/home',
        component: Container,
        children: [
            {
                path: 'home',
                name: 'Home',
                meta: {
                    requiresAuth: true
                },
                component: DashboardView
            },
            {
                path: 'registrations',
                name: 'Registrations',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/RegistrationsView.vue')
            },
            {
                path: 'users',
                name: 'Users',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/UsersView.vue')
            },
            {
                path: 'signatories',
                name: 'Signatories',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/SignatoriesView.vue')
            },
            {
                path: 'orders',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/TestOrdersView.vue'),
                children: [
                    {
                        path: 'donors',
                        name: 'DonorOrders',
                        component: () => import('@/components/TestOrder/DonorTestOrders.vue')
                    },
                    {
                        path: 'request',
                        name: 'RequestTestOrders',
                        component: () => import('@/components/TestOrder/RequestedTestOrders.vue')
                    }
                ]
            },
            {
                path: 'donors',
                name: 'Donors',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/DonorsView.vue')
            },
            {
                path: 'donor',
                meta: {
                    requiresAuth: true,
                },
                component: () => import('../views/DonorScreeningView.vue'),
                children: [
                    {
                        path: '',
                        component: () => import('../views/DonorScreeningView.vue'),
                    },
                    {
                        path: 'info/:reg_id',
                        name: 'DonorInformation',
                        component: () => import('@/components/DonorScreening/ScreeningForms/DonorInformation.vue'),
                        props: true//route => ({ params: route.params.donor_reg_id }) 
                    },
                    {
                        path: 'vitalsigns/:reg_id',
                        name: 'VitalSigns',
                        component: () => import('@/components/DonorScreening/ScreeningForms/VitalSigns.vue'),
                        props: true
                    },
                    {
                        path: 'initialscreening/:reg_id',
                        name: 'InitialScreening',
                        component: () => import('@/components/DonorScreening/ScreeningForms/InitialScreeningForm.vue'),
                        props: true
                    },
                    {
                        path: 'physicalexamination/:reg_id',
                        name: 'PhysicalExam',
                        component: () => import('@/components/DonorScreening/ScreeningForms/PhysicalExamForm.vue'),
                        props: true
                    },
                    {
                        path: 'counseling/:reg_id',
                        name: 'Counseling',
                        component: () => import('@/components/DonorScreening/ScreeningForms/Counseling.vue'),
                        props: true
                    },
                    {
                        path: 'consentform/:reg_id',
                        name: 'ConsentForm',
                        component: () => import('@/components/DonorScreening/ScreeningForms/ConsentForm.vue'),
                        props: true
                    },
                    {
                        path: 'methodbloodcollection/:reg_id',
                        name: 'ConsentForm',
                        component: () => import('@/components/DonorScreening/ScreeningForms/MethodOfBloodCollection.vue'),
                        props: true
                    },
                    {
                        path: 'issuanceofbloodbag/:reg_id',
                        name: 'ConsentForm',
                        component: () => import('@/components/DonorScreening/ScreeningForms/IssuanceOfBloodBag.vue'),
                        props: true
                    },
                    {
                        path: 'bloodcollection/:reg_id',
                        name: 'BloodCollection',
                        component: () => import('@/components/DonorScreening/ScreeningForms/BloodCollection.vue'),
                        props: true
                    },
                    {
                        path: 'postdonationcare/:reg_id',
                        name: 'PostDonationCare',
                        component: () => import('@/components/DonorScreening/ScreeningForms/PostDonationCare.vue'),
                        props: true
                    }
                ]
            },
            {
                path: 'schedules',
                name: 'Schedules',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/SchedulesView.vue'),
                //children: [
                //    {
                //        path: 'activitydonor/:schedule_id',
                //        name: 'ActivityDonor',
                //        component: () => import('@/components/Schedule/ScheduleForms/ActivityDonor.vue'),
                //        props: true//route => ({ params: route.params.donor_reg_id })
                //    }
                //]
                props:true
            },
            {
                path: '/schedules/activitydonor/:schedule_id',
                name: 'ActivityDonor',
                component: () => import('@/components/Schedule/ScheduleForms/ActivityDonor.vue'),
                props: true,
                meta: { requiresAuth: true }
            },
            {
                path: 'reports',
                name: 'Reports',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/ReportView.vue')
            },
            {
                path: 'inventory',
                name: 'Inventory',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/InventoryList.vue')
            },
            {
                path: 'patients',
                name: 'Patients',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/PatientsView.vue')
            },
            {
                path: 'requisitions',
                name: 'requisitions',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/RequisitionView.vue'),
                children: [
                    {
                        path: 'availability',
                        name: 'Availability',
                        component: () => import('@/components/Requisition/AvailabilityListing.vue')
                    },
                    {
                        path: 'reservations',
                        name: 'Reservations',
                        component: () => import('@/components/Requisition/ReservationListing.vue')
                    }
                ]
            },
            {
                path: 'app-settings',
                meta: {
                    requiresAuth: true
                },
                component: () => import('../views/ApplicationSettingsView.vue'),
                children: [
                    {
                        path: 'unit-of-measurement',
                        name: 'UnitOfMeasurementSetting',
                        component: () => import('@/components/ApplicationSetting/UnitOfMeasurementSetting.vue')
                    },
                    {
                        path: 'blood-component',
                        name: 'BloodComponentSetting',
                        component: () => import('@/components/ApplicationSetting/BloodComponentSetting.vue')
                    },
                    {
                        path: 'institutions',
                        name: 'InstitutionSetting',
                        component: () => import('@/components/ApplicationSetting/InstitutionSetting.vue')
                    },
                    {
                        path: 'blood-component-checklist',
                        name: 'BloodComponentChecklist',
                        component: () => import('@/components/ApplicationSetting/BloodComponentChecklist.vue')
                    },
                    {
                        path: 'test-order-type',
                        name: 'TestOrderType',
                        component: () => import('@/components/ApplicationSetting/TestOrderType.vue')
                    }
                ]
            },
            {
                path: '/forbidden',
                name: 'forbidden',
                meta: {
                    requiresAuth: false
                },
                component: () => import('../views/ForbiddenView.vue')
            }
        ]
    },
    {
        path: '/portal',
        name: 'portal',
        meta: {
            requiresAuth: false
        },
        component: () => import('../views/LandingPageView.vue')
    },
    {
        path: '/activitydonorregister/:schedule_id',
        name: 'ActivityDonorRegister',
        meta: {
            requiresAuth: false
        },
        component: () => import('@/components/Schedule/ScheduleForms/ActivityDonorRegister.vue'),
        props: true
    },
    {
        path: '/register',
        name: 'register',
        meta: {
            requiresAuth: false
        },
        component: () => import('../views/DonorRegisterView.vue')
    },
    {
        path: '/requisition',
        name: 'requisition',
        meta: {
            requiresAuth: false
        },
        component: () => import('../views/RequisitionView.vue')
    },
    {
        path: '/login',
        name: 'login',
        meta: {
            requiresAuth: false
        },
        component: () => import('../views/LoginView.vue')
    },
    {
        path: '*',
        name: 'notfound',
        meta: {
            requiresAuth: false
        },
        component: () => import('../views/NotFoundView.vue')
    },
]

const router = new VueRouter({
    routes
})

router.beforeEach((to, from, next) => {
    //   document.title = to.meta.title || 'Questel - Client Portal';
    let storage: BrowserStorageService = new BrowserStorageService();
    const token = storage.getItem(StorageKeysEnum.Token);

    if (to.name?.toLowerCase() === 'login' && token) {
        next({ path: 'home' });
    }
    else {
        const requiredAuth = to.matched.some(x => x.meta?.requiresAuth == true);
        const notLoginPath = to.name?.toLowerCase() !== 'login';
        const isPortalPath = to.name?.toLowerCase() === 'portal';
        storage.setItem("currentPath", to.name as string);
        if (requiredAuth && !token && notLoginPath && !isPortalPath) {
            next({ path: 'login' });
        } else {
            next();
        }
    }
});

export default router
