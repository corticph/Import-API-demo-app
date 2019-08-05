using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImportAPIClient.Manager
{
    public class ImportManager
    {
        private readonly Repo.ICaseRepo CaseRepo;
        private readonly Client.ImportAPIClient ImportAPIClient;

        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ImportManager(Repo.ICaseRepo CaseRepo, Client.ImportAPIClient ImportAPIClient)
        {
            this.CaseRepo = CaseRepo;
            this.ImportAPIClient = ImportAPIClient;
        }

        public void ImportAllCasesAndEvents() {
            var externalCasePropertiesToImport = new List<Client.Entity.CaseProperties>();
            var externalEventsToImport = new List<Client.Entity.CustomEvent>();
            var cases = this.CaseRepo.GetAllCases();

            logger.Debug(String.Format("Import started for {0} cases", cases.Count));
            foreach (var caseEntity in cases)
            {
                var externalCaseId = FindCaseIdByExternalId(caseEntity.ExternalId);
                if (externalCaseId == null) {
                    logger.Warn(String.Format("Can't find externalCaseId by externalId '{0}', skipping import of case with id {1}.", caseEntity.ExternalId, caseEntity.Id));
                    continue;
                }

                externalCasePropertiesToImport.Add(ConvertCaseToExternal(externalCaseId, caseEntity));
                externalEventsToImport.AddRange(ConvertEventsToExternal(externalCaseId, caseEntity.Events));
            }

            try
            {
                var importedCases = this.ImportAPIClient.ImportCasePropertiesAsync(externalCasePropertiesToImport).Result;
                logger.Debug("Imported cases:\n "+ JsonConvert.SerializeObject(importedCases));
            } catch(Exception e) {
                logger.Error(e, "Failed to import case properties");
                return;
            }

            try
            {
                var importedEvents = this.ImportAPIClient.ImportCustomEventsAsync(externalEventsToImport).Result;
                logger.Debug("Imported events:\n " + JsonConvert.SerializeObject(importedEvents));
            }
            catch (Exception e)
            {
                logger.Error(e, "Failed to import events");
                return;
            }

            logger.Debug("Import finished successfully");
        }

        private String FindCaseIdByExternalId(string externalId)
        {
            var sessions = ImportAPIClient.FindTriageSessionsByExternalIDAsync(externalId).Result;
            if (sessions.Count == 0)
            {
                return null;
            }

            return sessions[0].CaseId;
        }

        private Client.Entity.CaseProperties ConvertCaseToExternal(string externalCaseId, Entity.Case caseEntity)
        {
            return new Client.Entity.CaseProperties
            {
                CaseId = externalCaseId,
                Latitude = caseEntity.Latitude,
                Longitude = caseEntity.Longitude,
                CustomProperties = new Dictionary<string, string>()
                        {
                            { "division", caseEntity.Division },
                            { "batalion", caseEntity.Batalion },
                        }
            };
        }

        private Client.Entity.CustomEvent ConvertEventToExternal(string externalCaseId, Entity.Event eventEntity)
        {
            return new Client.Entity.CustomEvent
            {
                CaseId = externalCaseId,
                Name = eventEntity.Activity,
                Text = eventEntity.Comment,
                DatetimeDatetime = eventEntity.Datetime,
                CustomProperties = new Dictionary<string, string>()
                        {
                            { "radioName", eventEntity.RadioName },
                            { "dispatcherInit", eventEntity.DispatcherInit },
                        }
            };
        }

        private List<Client.Entity.CustomEvent> ConvertEventsToExternal(string externalCaseId, ICollection<Entity.Event> events)
        {
            var collection = new List<Client.Entity.CustomEvent>();
            foreach (var eventEntity in events)
            {
                collection.Add(ConvertEventToExternal(externalCaseId, eventEntity));
            }

            return collection;
        }
    }
}
