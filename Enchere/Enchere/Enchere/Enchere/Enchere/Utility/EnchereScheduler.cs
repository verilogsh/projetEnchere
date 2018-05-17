using System;
using Quartz;
using Quartz.Impl;
using Enchere.Dal;
using Enchere.Models;

namespace Enchere.Utility {
    public class StartScheduler {
        public void Start(DateTime date, string idObjet, string idEnchere) {

            //////// example test code  /////////////////////////////////
            //  string formatString = "yyyyMMddHHmmss";
            //  string sample = "20180514200910";
            //  DateTime dt = DateTime.ParseExact(sample, formatString, null);
            //
            //  Scheduler sc = new Scheduler();
            //  sc.Start(dt, "", "");
            //////////////////////////////////////////////////////////////

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<EnchereCommencer>().Build();
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity("IDGJob", "IDG")
               .StartAt(date)
               .UsingJobData("idObjet", idObjet)
               .UsingJobData("idEnchere", idEnchere)
               .WithPriority(1)
               .Build();
            scheduler.ScheduleJob(job, trigger);

        }
    }

    public class EnchereCommencer : IJob {
        public void Execute(IJobExecutionContext context) {
            ///// pass idObjet and IdEnchere  /////////
            var dataMap = context.MergedJobDataMap;
            string idEnchere = (string)dataMap["idEnchere"];
            string idObjet = (string)dataMap["idObjet"];

            ///// Taches pour commencer une enchere ///////////////
            ObjetRequette.setObjetEnVente(idObjet, 1);
            Encher en = EnchereRequette.getEnchereById(idEnchere);
            en.Etat = 0;
            EnchereRequette.updateEnchere(en);

        }
    }

    public class FinishScheduler {
        public void Start(DateTime date, string idObjet, string idEnchere) {

            //////// example test code  /////////////////////////////////
            //  string formatString = "yyyyMMddHHmmss";
            //  string sample = "20180514200910";
            //  DateTime dt = DateTime.ParseExact(sample, formatString, null);
            //
            //  Scheduler sc = new Scheduler();
            //  sc.Start(dt, "", "");
            //////////////////////////////////////////////////////////////

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<EnchereTerminer>().Build();
            ITrigger trigger = TriggerBuilder.Create()
             .WithIdentity("IDGJob1", "IDG1")
               .StartAt(date)
               .UsingJobData("idObjet", idObjet)
               .UsingJobData("idEnchere", idEnchere)
               .WithPriority(1)
               .Build();
            scheduler.ScheduleJob(job, trigger);

        }
    }

    public class EnchereTerminer : IJob {
            public void Execute(IJobExecutionContext context) {
                ///// pass idObjet and IdEnchere  /////////
                var dataMap = context.MergedJobDataMap;
                string idEnchere = (string)dataMap["idEnchere"];
                string idObjet = (string)dataMap["idObjet"];

            ///// Taches pour terminer une enchere ///////////////
            ObjetRequette.setObjetEnVente(idObjet, 0);
            Encher en = EnchereRequette.getEnchereById(idEnchere);
            if (en.IdAcheteur == en.IdVendeur) {
                EnchereRequette.setEnchereEtat(idEnchere, 2);
            } else {
                ObjetRequette.setObjetOwner(idObjet, en.IdAcheteur);
                EnchereRequette.setEnchereEtat(idEnchere, 1);
                int commission = Convert.ToInt16(CommissionRequette.ChercherCommission());
                CommissionRequette.insererCommissions(new Commissions(0, commission, en.PrixAchat, idEnchere, DateTime.Now));
            }
            ObjetRequette.setObjetEnVente(idObjet, 0);
        }

        }
    
}