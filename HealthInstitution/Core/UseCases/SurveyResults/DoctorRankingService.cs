﻿using System;
using System.Collections.Generic;
using HealthInstitution.Core.Repository;
using HealthInstitution.Core.Services.SurveyResults;
using HealthInstitution.Repositories;

namespace HealthInstitution.Core
{
    public class DoctorRankingService : IDoctorRankingService
    {
        private IDoctorSuvreyResultsService _surveyResults;
        private IDoctorRepositoryService _doctorService;

        public DoctorRankingService()
        {
            _surveyResults = new DoctorSurveyResultsService();
            _doctorService = new DoctorRepositoryService();
        }

        public List<Tuple<Doctor, double>> GetBest()
        {
            List<Tuple<Doctor, double>> bestDoctors = new List<Tuple<Doctor, double>>() { null, null, null };
            

            foreach (Doctor doctor in _doctorService.GetDoctors())
            {
                Dictionary<string, List<double>> score = _surveyResults.GetResults(doctor);
                if (double.IsNaN(score["Service"][0]))
                {
                    continue;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (bestDoctors[i] is null)
                    {
                        bestDoctors[i] = new Tuple<Doctor, double>(doctor, score["Service"][0]);
                        break;
                    } else if (bestDoctors[i].Item2 < score["Service"][0])
                    {
                        for (int j = 2; j > i; j--)
                        {
                            bestDoctors[j] = bestDoctors[j - 1];
                        }
                        bestDoctors[i] = new Tuple<Doctor, double>(doctor, score["Service"][0]);
                        break;
                    }
                }
            }

            return bestDoctors;
        }

        public List<Tuple<Doctor, double>> GetWorst()
        {
            List<Tuple<Doctor, double>> worstDoctors = new List<Tuple<Doctor, double>>() { null, null, null };


            foreach (Doctor doctor in _doctorService.GetDoctors())
            {
                Dictionary<string, List<double>> score = _surveyResults.GetResults(doctor);
                if (double.IsNaN(score["Service"][0]))
                {
                    continue;
                }
                for (int i = 0; i < 3; i++)
                {
                    if (worstDoctors[i] is null)
                    {
                        worstDoctors[i] = new Tuple<Doctor, double>(doctor, score["Service"][0]);
                        break;
                    }
                    else if (worstDoctors[i].Item2 > score["Service"][0])
                    {
                        for (int j = 2; j > i; j--)
                        {
                            worstDoctors[j] = worstDoctors[j - 1];
                        }
                        worstDoctors[i] = new Tuple<Doctor, double>(doctor, score["Service"][0]);
                        break;
                    }
                }
            }

            return worstDoctors;
        }
    }
}