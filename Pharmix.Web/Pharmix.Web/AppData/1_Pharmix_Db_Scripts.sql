/****** Object:  ForeignKey [FK_Addresses_AddressType_AddressTypeId]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Addresses_AddressType_AddressTypeId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Addresses]'))
ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_Addresses_AddressType_AddressTypeId]
GO
/****** Object:  ForeignKey [FK_dbo.AdministrationMethod_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AdministrationMethod_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]'))
ALTER TABLE [dbo].[AdministrationMethod] DROP CONSTRAINT [FK_dbo.AdministrationMethod_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_AspNetRoleClaims_AspNetRoles_RoleId]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_AspNetUserLogins_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_AspNetUserRoles_AspNetRoles_RoleId]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_AspNetUserRoles_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_AspNetUserTokens_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] DROP CONSTRAINT [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]
GO
/****** Object:  ForeignKey [FK_dbo.DosageForm_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] DROP CONSTRAINT [FK_dbo.DosageForm_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_dbo.DosageFormType_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageFormType_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageFormType]'))
ALTER TABLE [dbo].[DosageFormType] DROP CONSTRAINT [FK_dbo.DosageFormType_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Manufacturer_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Manufacturer_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Manufacturer]'))
ALTER TABLE [dbo].[Manufacturer] DROP CONSTRAINT [FK_dbo.Manufacturer_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_Permission_Module_ModuleId]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Module_ModuleId]
GO
/****** Object:  ForeignKey [FK_Permission_UserModule_UserModuleId]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_UserModule_UserModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_UserModule_UserModuleId]
GO
/****** Object:  ForeignKey [FK_PermissionGroup_Permission_PermissionId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionGroup_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionGroup]'))
ALTER TABLE [dbo].[PermissionGroup] DROP CONSTRAINT [FK_PermissionGroup_Permission_PermissionId]
GO
/****** Object:  ForeignKey [FK_RolePermission_Permission_PermissionId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permission_PermissionId]
GO
/****** Object:  ForeignKey [FK_RolePermission_PermissionGroup_PermissionGroupId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_PermissionGroup_PermissionGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_PermissionGroup_PermissionGroupId]
GO
/****** Object:  ForeignKey [FK_TrustAddresses_Addresses_AddressId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Addresses_AddressId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] DROP CONSTRAINT [FK_TrustAddresses_Addresses_AddressId]
GO
/****** Object:  ForeignKey [FK_TrustAddresses_Trusts_TrustId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] DROP CONSTRAINT [FK_TrustAddresses_Trusts_TrustId]
GO
/****** Object:  ForeignKey [FK_TrustContacts_Contacts_ContactId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Contacts_ContactId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] DROP CONSTRAINT [FK_TrustContacts_Contacts_ContactId]
GO
/****** Object:  ForeignKey [FK_TrustContacts_Trusts_TrustId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] DROP CONSTRAINT [FK_TrustContacts_Trusts_TrustId]
GO
/****** Object:  ForeignKey [FK_UserModule_Module_ModuleId]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserModule_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserModule]'))
ALTER TABLE [dbo].[UserModule] DROP CONSTRAINT [FK_UserModule_Module_ModuleId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitation_Reminder_ReminderId]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitation_Reminder_ReminderId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitation]'))
ALTER TABLE [task].[ReminderInvitation] DROP CONSTRAINT [FK_ReminderInvitation_Reminder_ReminderId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] DROP CONSTRAINT [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] DROP CONSTRAINT [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]
GO
/****** Object:  ForeignKey [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderProgress]'))
ALTER TABLE [task].[ReminderProgress] DROP CONSTRAINT [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_Permission_PermissionId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_PermissionGroup_PermissionGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] DROP CONSTRAINT [FK_RolePermission_PermissionGroup_PermissionGroupId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission]') AND type in (N'U'))
DROP TABLE [dbo].[RolePermission]
GO
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionGroup_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionGroup]'))
ALTER TABLE [dbo].[PermissionGroup] DROP CONSTRAINT [FK_PermissionGroup_Permission_PermissionId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PermissionGroup]') AND type in (N'U'))
DROP TABLE [dbo].[PermissionGroup]
GO
/****** Object:  Table [dbo].[DosageForm]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] DROP CONSTRAINT [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] DROP CONSTRAINT [FK_dbo.DosageForm_dbo.Status_Status_Id]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DosageForm]') AND type in (N'U'))
DROP TABLE [dbo].[DosageForm]
GO
/****** Object:  Table [dbo].[TrustAddresses]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Addresses_AddressId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] DROP CONSTRAINT [FK_TrustAddresses_Addresses_AddressId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] DROP CONSTRAINT [FK_TrustAddresses_Trusts_TrustId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrustAddresses]') AND type in (N'U'))
DROP TABLE [dbo].[TrustAddresses]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_Module_ModuleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_UserModule_UserModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] DROP CONSTRAINT [FK_Permission_UserModule_UserModuleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
DROP TABLE [dbo].[Permission]
GO
/****** Object:  Table [task].[ReminderInvitationMember]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] DROP CONSTRAINT [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] DROP CONSTRAINT [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]') AND type in (N'U'))
DROP TABLE [task].[ReminderInvitationMember]
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLFileToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLFileToChangeset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DmdProcessXMLFileToChangeset]
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLToStaging]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLToStaging]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DmdProcessXMLToStaging]
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLToStaging_01]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLToStaging_01]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DmdProcessXMLToStaging_01]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDmdAmpHistory]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetDmdAmpHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetDmdAmpHistory]
GO
/****** Object:  Table [task].[ReminderProgress]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderProgress]'))
ALTER TABLE [task].[ReminderProgress] DROP CONSTRAINT [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderProgress]') AND type in (N'U'))
DROP TABLE [task].[ReminderProgress]
GO
/****** Object:  Table [task].[ReminderInvitation]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitation_Reminder_ReminderId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitation]'))
ALTER TABLE [task].[ReminderInvitation] DROP CONSTRAINT [FK_ReminderInvitation_Reminder_ReminderId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderInvitation]') AND type in (N'U'))
DROP TABLE [task].[ReminderInvitation]
GO
/****** Object:  Table [dbo].[TrustContacts]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Contacts_ContactId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] DROP CONSTRAINT [FK_TrustContacts_Contacts_ContactId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] DROP CONSTRAINT [FK_TrustContacts_Trusts_TrustId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrustContacts]') AND type in (N'U'))
DROP TABLE [dbo].[TrustContacts]
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessStagingToHistory]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessStagingToHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DmdProcessStagingToHistory]
GO
/****** Object:  Table [dbo].[UserModule]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserModule_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserModule]'))
ALTER TABLE [dbo].[UserModule] DROP CONSTRAINT [FK_UserModule_Module_ModuleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserModule]') AND type in (N'U'))
DROP TABLE [dbo].[UserModule]
GO
/****** Object:  Table [dbo].[DosageFormType]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageFormType_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageFormType]'))
ALTER TABLE [dbo].[DosageFormType] DROP CONSTRAINT [FK_dbo.DosageFormType_dbo.Status_Status_Id]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DosageFormType]') AND type in (N'U'))
DROP TABLE [dbo].[DosageFormType]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Addresses_AddressType_AddressTypeId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Addresses]'))
ALTER TABLE [dbo].[Addresses] DROP CONSTRAINT [FK_Addresses_AddressType_AddressTypeId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Addresses]') AND type in (N'U'))
DROP TABLE [dbo].[Addresses]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims] DROP CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoleClaims]
GO
/****** Object:  StoredProcedure [dbo].[ProcessDmdDataToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcessDmdDataToChangeset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ProcessDmdDataToChangeset]
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Manufacturer_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Manufacturer]'))
ALTER TABLE [dbo].[Manufacturer] DROP CONSTRAINT [FK_dbo.Manufacturer_dbo.Status_Status_Id]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND type in (N'U'))
DROP TABLE [dbo].[Manufacturer]
GO
/****** Object:  Table [dbo].[AdministrationMethod]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AdministrationMethod_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]'))
ALTER TABLE [dbo].[AdministrationMethod] DROP CONSTRAINT [FK_dbo.AdministrationMethod_dbo.Status_Status_Id]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]') AND type in (N'U'))
DROP TABLE [dbo].[AdministrationMethod]
GO
/****** Object:  StoredProcedure [dbo].[DmdProcessXMLFileToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DmdProcessXMLFileToChangeset]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DmdProcessXMLFileToChangeset]
GO
/****** Object:  StoredProcedure [dbo].[GetDmdReports]    Script Date: 07/14/2018 21:15:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDmdReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDmdReports]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_AspNetUserLogins_IdentityUser_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_IdentityUser_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens] DROP CONSTRAINT [FK_AspNetUserTokens_IdentityUser_UserId]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserTokens]
GO
/****** Object:  Table [dbo].[BusinessDetails]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessDetails]') AND type in (N'U'))
DROP TABLE [dbo].[BusinessDetails]
GO
/****** Object:  Table [dbo].[BusinessUser]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessUser]') AND type in (N'U'))
DROP TABLE [dbo].[BusinessUser]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contacts]') AND type in (N'U'))
DROP TABLE [dbo].[Contacts]
GO
/****** Object:  Table [dbo].[CustomerTB]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerTB]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerTB]
GO
/****** Object:  Table [dbo].[Dmd_AdministrationMethod]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_AdministrationMethod]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_AdministrationMethod]
GO
/****** Object:  Table [dbo].[Dmd_Amp]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Amp]
GO
/****** Object:  Table [dbo].[Dmd_Amp_Changeset]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Amp_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Amp_Changeset_Backup]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Amp_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Amp_History]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Amp_History]
GO
/****** Object:  Table [dbo].[Dmd_Ampp]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Ampp]
GO
/****** Object:  Table [dbo].[Dmd_Ampp_Changeset]    Script Date: 07/14/2018 21:15:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Ampp_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Ampp_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Ampp_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Ampp_History]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Ampp_History]
GO
/****** Object:  Table [dbo].[Dmd_BusinessChangeSetDetails]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Dmd_Busin__Creat__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dmd_BusinessChangeSetDetails] DROP CONSTRAINT [DF__Dmd_Busin__Creat__6A30C649]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_BusinessChangeSetDetails]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_BusinessChangeSetDetails]
GO
/****** Object:  Table [dbo].[Dmd_ChangeSetDetails]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Dmd_Chang__Isdel__656C112C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dmd_ChangeSetDetails] DROP CONSTRAINT [DF__Dmd_Chang__Isdel__656C112C]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Dmd_Chang__Creat__66603565]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dmd_ChangeSetDetails] DROP CONSTRAINT [DF__Dmd_Chang__Creat__66603565]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Dmd_Chang__Ispro__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dmd_ChangeSetDetails] DROP CONSTRAINT [DF__Dmd_Chang__Ispro__6754599E]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_ChangeSetDetails]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_ChangeSetDetails]
GO
/****** Object:  Table [dbo].[Dmd_DosageForm]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_DosageForm]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_DosageForm]
GO
/****** Object:  Table [dbo].[Dmd_Form]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Form]
GO
/****** Object:  Table [dbo].[Dmd_Form_Changeset]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Form_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Form_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Form_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Form_History]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Form_History]
GO
/****** Object:  Table [dbo].[Dmd_FTPFileDownloadDetails]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Dmd_FTPFi__Creat__59FA5E80]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Dmd_FTPFileDownloadDetails] DROP CONSTRAINT [DF__Dmd_FTPFi__Creat__59FA5E80]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_FTPFileDownloadDetails]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_FTPFileDownloadDetails]
GO
/****** Object:  Table [dbo].[Dmd_Gtin]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Gtin]
GO
/****** Object:  Table [dbo].[Dmd_Gtin_Changeset]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Gtin_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Gtin_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Gtin_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Gtin_History]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Gtin_History]
GO
/****** Object:  Table [dbo].[Dmd_Link_AmppSequence]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_AmppSequence]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Link_AmppSequence]
GO
/****** Object:  Table [dbo].[Dmd_Link_GtinDetail]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_GtinDetail]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Link_GtinDetail]
GO
/****** Object:  Table [dbo].[Dmd_Link_GtinSequence]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_GtinSequence]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Link_GtinSequence]
GO
/****** Object:  Table [dbo].[Dmd_Manufacturer]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Manufacturer]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Manufacturer]
GO
/****** Object:  Table [dbo].[Dmd_Route]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Route]
GO
/****** Object:  Table [dbo].[Dmd_Route_Changeset]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Route_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Route_Changeset_Backup]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Route_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Route_History]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Route_History]
GO
/****** Object:  Table [dbo].[Dmd_Supplier]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Supplier]
GO
/****** Object:  Table [dbo].[Dmd_Supplier_Changeset]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Supplier_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Supplier_Changeset_Backup]    Script Date: 07/14/2018 21:15:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Supplier_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Supplier_History]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Supplier_History]
GO
/****** Object:  Table [dbo].[Dmd_Vmp]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changeset]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changeset_Backup]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changset]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_Changset]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Form]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Form]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_Form]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_History]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_History]
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Route]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Route]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmp_Route]
GO
/****** Object:  Table [dbo].[Dmd_Vmpp]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmpp]
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_Changeset]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmpp_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_Changeset_Backup]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmpp_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_History]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vmpp_History]
GO
/****** Object:  Table [dbo].[Dmd_Vtm]    Script Date: 07/14/2018 21:15:49 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vtm]
GO
/****** Object:  Table [dbo].[Dmd_Vtm_Changeset]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_Changeset]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vtm_Changeset]
GO
/****** Object:  Table [dbo].[Dmd_Vtm_Changeset_Backup]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_Changeset_Backup]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vtm_Changeset_Backup]
GO
/****** Object:  Table [dbo].[Dmd_Vtm_History]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_History]') AND type in (N'U'))
DROP TABLE [dbo].[Dmd_Vtm_History]
GO
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityUser]') AND type in (N'U'))
DROP TABLE [dbo].[IdentityUser]
GO
/****** Object:  Table [dbo].[AmppReference]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AmppReference]') AND type in (N'U'))
DROP TABLE [dbo].[AmppReference]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
DROP TABLE [dbo].[Module]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO
/****** Object:  Table [task].[Reminder]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[Reminder]') AND type in (N'U'))
DROP TABLE [task].[Reminder]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 07/14/2018 21:15:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddressType]') AND type in (N'U'))
DROP TABLE [dbo].[AddressType]
GO
/****** Object:  Table [dbo].[EmpDup]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpDup]') AND type in (N'U'))
DROP TABLE [dbo].[EmpDup]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeDetails]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeDetails]
GO
/****** Object:  Table [dbo].[EmployeeSalary]    Script Date: 07/14/2018 21:15:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeSalary]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeSalary]
GO
/****** Object:  UserDefinedFunction [dbo].[fc_FileExists]    Script Date: 07/14/2018 21:15:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fc_FileExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fc_FileExists]
GO
/****** Object:  Table [dbo].[Sites]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sites]') AND type in (N'U'))
DROP TABLE [dbo].[Sites]
GO
/****** Object:  Table [trusts].[Trusts]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[trusts].[Trusts]') AND type in (N'U'))
DROP TABLE [trusts].[Trusts]
GO
/****** Object:  Table [dbo].[UpdatedProducts]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatedProducts]') AND type in (N'U'))
DROP TABLE [dbo].[UpdatedProducts]
GO
/****** Object:  Table [dbo].[UploadedFiles]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__UploadedF__Isdel__07F6335A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UploadedFiles] DROP CONSTRAINT [DF__UploadedF__Isdel__07F6335A]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__UploadedF__Ispro__08EA5793]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UploadedFiles] DROP CONSTRAINT [DF__UploadedF__Ispro__08EA5793]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedFiles]') AND type in (N'U'))
DROP TABLE [dbo].[UploadedFiles]
GO
/****** Object:  Table [dbo].[UploadedFiles1]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedFiles1]') AND type in (N'U'))
DROP TABLE [dbo].[UploadedFiles1]
GO
/****** Object:  Table [task].[ReminderProgressStatus]    Script Date: 07/14/2018 21:15:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderProgressStatus]') AND type in (N'U'))
DROP TABLE [task].[ReminderProgressStatus]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07/14/2018 21:15:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
DROP TABLE [dbo].[Status]
GO
/****** Object:  Schema [task]    Script Date: 07/14/2018 21:15:41 ******/
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'task')
DROP SCHEMA [task]
GO
/****** Object:  Schema [trusts]    Script Date: 07/14/2018 21:15:41 ******/
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'trusts')
DROP SCHEMA [trusts]
GO
/****** Object:  Schema [task]    Script Date: 07/14/2018 21:15:41 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'task')
EXEC sys.sp_executesql N'CREATE SCHEMA [task] AUTHORIZATION [dbo]'
GO
/****** Object:  Schema [trusts]    Script Date: 07/14/2018 21:15:41 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'trusts')
EXEC sys.sp_executesql N'CREATE SCHEMA [trusts] AUTHORIZATION [dbo]'
GO
/****** Object:  Table [dbo].[Status]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Status]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [task].[ReminderProgressStatus]    Script Date: 07/14/2018 21:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderProgressStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [task].[ReminderProgressStatus](
	[ReminderProgressStatusId] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReminderProgressStatus] PRIMARY KEY CLUSTERED 
(
	[ReminderProgressStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UploadedFiles1]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedFiles1]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UploadedFiles1](
	[UploadedFileID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](100) NULL,
	[FilePath] [nvarchar](max) NULL,
	[XmlData] [xml] NULL,
	[ChangesetID] [varchar](500) NULL,
	[CreatedOn] [datetime] NULL,
	[Isdeleted] [bit] NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UploadedFiles]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UploadedFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UploadedFiles](
	[UploadedFileID] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [varchar](100) NULL,
	[FilePath] [nvarchar](max) NULL,
	[XmlData] [xml] NULL,
	[ChangesetID] [varchar](500) NULL,
	[WeekNo] [varchar](50) NULL,
	[YearNo] [varchar](50) NULL,
	[ChangesetFileSize] [decimal](9, 2) NULL,
	[CreatedOn] [datetime] NULL,
	[Isdeleted] [bit] NULL DEFAULT ((0)),
	[Isprocessed] [bit] NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[UploadedFileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UpdatedProducts]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatedProducts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UpdatedProducts](
	[ProductID] [int] NOT NULL,
	[ProductName] [varchar](100) NULL,
	[Rate] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [trusts].[Trusts]    Script Date: 07/14/2018 21:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[trusts].[Trusts]') AND type in (N'U'))
BEGIN
CREATE TABLE [trusts].[Trusts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[FridayOpenTiming] [nvarchar](12) NULL,
	[IsArchived] [bit] NOT NULL,
	[LogoImage] [varbinary](max) NULL,
	[LogoImageName] [nvarchar](200) NULL,
	[MondayOpenTiming] [nvarchar](12) NULL,
	[Name] [nvarchar](200) NOT NULL,
	[SaturdayOpenTiming] [nvarchar](12) NULL,
	[SundayOpenTiming] [nvarchar](12) NULL,
	[ThursdayOpenTiming] [nvarchar](12) NULL,
	[TuesdayOpenTiming] [nvarchar](12) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[WednesdayOpenTiming] [nvarchar](12) NULL,
 CONSTRAINT [PK_Trusts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Sites]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sites]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Sites](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  UserDefinedFunction [dbo].[fc_FileExists]    Script Date: 07/14/2018 21:15:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fc_FileExists]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'	  create FUNCTION [dbo].[fc_FileExists](@path varchar(8000))
RETURNS BIT
AS
BEGIN
     DECLARE @result INT
     EXEC master.dbo.xp_fileexist @path, @result OUTPUT
     RETURN cast(@result as bit)
END;

' 
END
GO
/****** Object:  Table [dbo].[EmployeeSalary]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeSalary]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeSalary](
	[EmpID] [int] NULL,
	[Salary] [int] NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmployeeDetails]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeDetails](
	[EmployeeId] [int] NULL,
	[Name] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Others] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[EmpID] [int] NULL,
	[EmpName] [varchar](15) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EmpDup]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpDup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmpDup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[empid] [int] NULL,
	[name] [varchar](20) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AddressType]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddressType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AddressType](
	[Id] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_AddressType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [task].[Reminder]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[Reminder]') AND type in (N'U'))
BEGIN
CREATE TABLE [task].[Reminder](
	[ReminderId] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[DueDate] [datetime2](7) NULL,
	[FromDateTime] [datetime2](7) NULL,
	[IsArchived] [bit] NOT NULL,
	[Location] [nvarchar](max) NULL,
	[ReminderEntityTypeId] [int] NOT NULL,
	[ReminderModuleId] [int] NOT NULL,
	[ReminderModuleKeyId] [nvarchar](max) NULL,
	[ReminderTypeId] [int] NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[TodDateTime] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_Reminder] PRIMARY KEY CLUSTERED 
(
	[ReminderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Products]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[ProductName] [varchar](100) NULL,
	[Rate] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Module](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[Description] [nvarchar](500) NOT NULL,
	[IsArchived] [bit] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AmppReference]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AmppReference]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AmppReference](
	[AmppReferenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[AmppId] [bigint] NOT NULL,
	[AmppName] [varchar](1000) NOT NULL,
	[AmpId] [bigint] NULL,
	[AmpName] [varchar](1000) NULL,
	[VmpId] [bigint] NULL,
	[VmpName] [varchar](1000) NULL,
	[VtmId] [bigint] NULL,
	[VtmName] [varchar](1000) NULL,
	[ManufacturerId] [bigint] NULL,
	[ManufacturerName] [varchar](1000) NULL,
	[GtinCsv] [varchar](1000) NULL,
	[FormIdCsv] [varchar](1000) NULL,
	[FormNameCsv] [varchar](1000) NULL,
	[RouteIdCsv] [varchar](1000) NULL,
	[RouteNameCsv] [varchar](1000) NULL,
 CONSTRAINT [PK_AmppReference] PRIMARY KEY CLUSTERED 
(
	[AmppReferenceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[IdentityUser]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IdentityUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[IdentityUser](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[IsApproved] [bit] NULL,
	[GenderId] [int] NULL,
	[DateOfBirth] [datetime] NULL,
	[FirstName] [varchar](100) NULL,
	[Surname] [varchar](100) NULL,
	[MobileNumber] [varchar](100) NULL,
	[AlternativeTel] [varchar](100) NULL,
	[IsResetPasswordRequired] [bit] NULL,
	[AddressId] [int] NULL,
 CONSTRAINT [PK_IdentityUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Vtm_History]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vtm_History](
	[VtmHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[VTMIDLive] [nvarchar](max) NULL,
	[INVALIDLive] [nvarchar](max) NULL,
	[NMLive] [nvarchar](max) NULL,
	[ABBREVNMLive] [nvarchar](max) NULL,
	[VTMIDPREVLive] [nvarchar](max) NULL,
	[VTMIDDTLive] [nvarchar](max) NULL,
	[VTMIDChangeset] [nvarchar](max) NULL,
	[INVALIDChangeset] [nvarchar](max) NULL,
	[NMChangeset] [nvarchar](max) NULL,
	[ABBREVNMChangeset] [nvarchar](max) NULL,
	[VTMIDPREVChangeset] [nvarchar](max) NULL,
	[VTMIDDTChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_VtmHistoryId] PRIMARY KEY CLUSTERED 
(
	[VtmHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Vtm_Changeset_Backup]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vtm_Changeset_Backup](
	[VtmChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[VTMID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VTMIDPREV] [nvarchar](max) NULL,
	[VTMIDDT] [nvarchar](max) NULL,
	[ChangsetId] [nvarchar](max) NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VtmChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[VtmChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vtm_Changeset]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vtm_Changeset](
	[VtmChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[VTMID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VTMIDPREV] [nvarchar](max) NULL,
	[VTMIDDT] [nvarchar](max) NULL,
	[ChangsetId] [nvarchar](max) NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VtmChangesetId] PRIMARY KEY CLUSTERED 
(
	[VtmChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vtm]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vtm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vtm](
	[VTMID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VTMIDPREV] [nvarchar](max) NULL,
	[VTMIDDT] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_History]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmpp_History](
	[VmppHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[VPPIDLive] [nvarchar](max) NULL,
	[INVALIDLive] [nvarchar](max) NULL,
	[NMLive] [nvarchar](max) NULL,
	[ABBREVNMLive] [nvarchar](max) NULL,
	[VPIDLive] [nvarchar](max) NULL,
	[QTYVALLive] [nvarchar](max) NULL,
	[QTY_UOMCDLive] [nvarchar](max) NULL,
	[COMBPACKCDLive] [nvarchar](max) NULL,
	[VMPPS_IdLive] [nvarchar](max) NULL,
	[VPPIDChangeset] [nvarchar](max) NULL,
	[INVALIDChangeset] [nvarchar](max) NULL,
	[NMChangeset] [nvarchar](max) NULL,
	[ABBREVNMChangeset] [nvarchar](max) NULL,
	[VPIDChangeset] [nvarchar](max) NULL,
	[QTYVALChangeset] [nvarchar](max) NULL,
	[QTY_UOMCDChangeset] [nvarchar](max) NULL,
	[COMBPACKCDChangeset] [nvarchar](max) NULL,
	[VMPPS_IdChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_VmppHistoryId] PRIMARY KEY CLUSTERED 
(
	[VmppHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_Changeset_Backup]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmpp_Changeset_Backup](
	[VmppChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[VPPID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[QTYVAL] [nvarchar](max) NULL,
	[QTY_UOMCD] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[VMPPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [nvarchar](max) NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VmppChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[VmppChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmpp_Changeset]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmpp_Changeset](
	[VmppChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[VPPID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[QTYVAL] [nvarchar](max) NULL,
	[QTY_UOMCD] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[VMPPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [nvarchar](max) NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VmppChangesetId] PRIMARY KEY CLUSTERED 
(
	[VmppChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmpp]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmpp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmpp](
	[VPPID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[QTYVAL] [nvarchar](max) NULL,
	[QTY_UOMCD] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[VMPPS_Id] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Route]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Route]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_Route](
	[VPID] [bigint] NULL,
	[ROUTECD] [bigint] NULL,
	[DRUG_ROUTE_Id] [numeric](20, 0) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp_History]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_History](
	[VmpHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[VPIDLive] [nvarchar](max) NULL,
	[VPIDDTLive] [nvarchar](max) NULL,
	[VPIDPREVLive] [nvarchar](max) NULL,
	[VTMIDLive] [nvarchar](max) NULL,
	[INVALIDLive] [nvarchar](max) NULL,
	[NMLive] [nvarchar](max) NULL,
	[ABBREVNMLive] [nvarchar](max) NULL,
	[BASISCDLive] [nvarchar](max) NULL,
	[NMDTLive] [nvarchar](max) NULL,
	[NMPREVLive] [nvarchar](max) NULL,
	[BASIS_PREVCDLive] [nvarchar](max) NULL,
	[NMCHANGECDLive] [nvarchar](max) NULL,
	[COMBPRODCDLive] [nvarchar](max) NULL,
	[PRES_STATCDLive] [nvarchar](max) NULL,
	[SUG_FLive] [nvarchar](max) NULL,
	[GLU_FLive] [nvarchar](max) NULL,
	[PRES_FLive] [nvarchar](max) NULL,
	[CFC_FLive] [nvarchar](max) NULL,
	[NON_AVAILCDLive] [nvarchar](max) NULL,
	[NON_AVAILDTLive] [nvarchar](max) NULL,
	[DF_INDCDLive] [nvarchar](max) NULL,
	[UDFSLive] [nvarchar](max) NULL,
	[UDFS_UOMCDLive] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCDLive] [nvarchar](max) NULL,
	[VMPS_IdLive] [nvarchar](max) NULL,
	[VPIDChangeset] [nvarchar](max) NULL,
	[VPIDDTChangeset] [nvarchar](max) NULL,
	[VPIDPREVChangeset] [nvarchar](max) NULL,
	[VTMIDChangeset] [nvarchar](max) NULL,
	[INVALIDChangeset] [nvarchar](max) NULL,
	[NMChangeset] [nvarchar](max) NULL,
	[ABBREVNMChangeset] [nvarchar](max) NULL,
	[BASISCDChangeset] [nvarchar](max) NULL,
	[NMDTChangeset] [nvarchar](max) NULL,
	[NMPREVChangeset] [nvarchar](max) NULL,
	[BASISChangeset_PREVCD] [nvarchar](max) NULL,
	[NMCHANGECDChangeset] [nvarchar](max) NULL,
	[COMBPRODCDChangeset] [nvarchar](max) NULL,
	[PRES_STATCDChangeset] [nvarchar](max) NULL,
	[SUG_FChangeset] [nvarchar](max) NULL,
	[GLU_FChangeset] [nvarchar](max) NULL,
	[PRES_FChangeset] [nvarchar](max) NULL,
	[CFC_FChangeset] [nvarchar](max) NULL,
	[NON_AVAILCDChangeset] [nvarchar](max) NULL,
	[NON_AVAILDTChangeset] [nvarchar](max) NULL,
	[DF_INDCDChangeset] [nvarchar](max) NULL,
	[UDFSChangeset] [nvarchar](max) NULL,
	[UDFS_UOMCDChangeset] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCDChangeset] [nvarchar](max) NULL,
	[VMPS_IdChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_VmpHistoryId] PRIMARY KEY CLUSTERED 
(
	[VmpHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Form]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Form]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_Form](
	[VPID] [nvarchar](max) NOT NULL,
	[FORMCD] [nvarchar](max) NOT NULL,
	[DRUG_FORM_Id] [numeric](20, 0) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changset]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_Changset](
	[VPID_Changset] [bigint] NOT NULL,
	[VPIDDT] [nvarchar](max) NULL,
	[VPIDPREV] [nvarchar](max) NULL,
	[VTMID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[BASISCD] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NMPREV] [nvarchar](max) NULL,
	[BASIS_PREVCD] [nvarchar](max) NULL,
	[NMCHANGECD] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[PRES_STATCD] [nvarchar](max) NULL,
	[SUG_F] [nvarchar](max) NULL,
	[GLU_F] [nvarchar](max) NULL,
	[PRES_F] [nvarchar](max) NULL,
	[CFC_F] [nvarchar](max) NULL,
	[NON_AVAILCD] [nvarchar](max) NULL,
	[NON_AVAILDT] [nvarchar](max) NULL,
	[DF_INDCD] [nvarchar](max) NULL,
	[UDFS] [nvarchar](max) NULL,
	[UDFS_UOMCD] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCD] [nvarchar](max) NULL,
	[VMPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_Vmp_Changset] PRIMARY KEY CLUSTERED 
(
	[VPID_Changset] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changeset_Backup]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_Changeset_Backup](
	[VmpChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[VPID] [nvarchar](max) NOT NULL,
	[VPIDDT] [nvarchar](max) NULL,
	[VPIDPREV] [nvarchar](max) NULL,
	[VTMID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[BASISCD] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NMPREV] [nvarchar](max) NULL,
	[BASIS_PREVCD] [nvarchar](max) NULL,
	[NMCHANGECD] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[PRES_STATCD] [nvarchar](max) NULL,
	[SUG_F] [nvarchar](max) NULL,
	[GLU_F] [nvarchar](max) NULL,
	[PRES_F] [nvarchar](max) NULL,
	[CFC_F] [nvarchar](max) NULL,
	[NON_AVAILCD] [nvarchar](max) NULL,
	[NON_AVAILDT] [nvarchar](max) NULL,
	[DF_INDCD] [nvarchar](max) NULL,
	[UDFS] [nvarchar](max) NULL,
	[UDFS_UOMCD] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCD] [nvarchar](max) NULL,
	[VMPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [nvarchar](max) NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VmpChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[VmpChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp_Changeset]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp_Changeset](
	[VmpChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[VPID] [nvarchar](max) NOT NULL,
	[VPIDDT] [nvarchar](max) NULL,
	[VPIDPREV] [nvarchar](max) NULL,
	[VTMID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[BASISCD] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NMPREV] [nvarchar](max) NULL,
	[BASIS_PREVCD] [nvarchar](max) NULL,
	[NMCHANGECD] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[PRES_STATCD] [nvarchar](max) NULL,
	[SUG_F] [nvarchar](max) NULL,
	[GLU_F] [nvarchar](max) NULL,
	[PRES_F] [nvarchar](max) NULL,
	[CFC_F] [nvarchar](max) NULL,
	[NON_AVAILCD] [nvarchar](max) NULL,
	[NON_AVAILDT] [nvarchar](max) NULL,
	[DF_INDCD] [nvarchar](max) NULL,
	[UDFS] [nvarchar](max) NULL,
	[UDFS_UOMCD] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCD] [nvarchar](max) NULL,
	[VMPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_VmpChangesetId] PRIMARY KEY CLUSTERED 
(
	[VmpChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Vmp]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Vmp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Vmp](
	[VPID] [nvarchar](max) NOT NULL,
	[VPIDDT] [nvarchar](max) NULL,
	[VPIDPREV] [nvarchar](max) NULL,
	[VTMID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[BASISCD] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NMPREV] [nvarchar](max) NULL,
	[BASIS_PREVCD] [nvarchar](max) NULL,
	[NMCHANGECD] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[PRES_STATCD] [nvarchar](max) NULL,
	[SUG_F] [nvarchar](max) NULL,
	[GLU_F] [nvarchar](max) NULL,
	[PRES_F] [nvarchar](max) NULL,
	[CFC_F] [nvarchar](max) NULL,
	[NON_AVAILCD] [nvarchar](max) NULL,
	[NON_AVAILDT] [nvarchar](max) NULL,
	[DF_INDCD] [nvarchar](max) NULL,
	[UDFS] [nvarchar](max) NULL,
	[UDFS_UOMCD] [nvarchar](max) NULL,
	[UNIT_DOSE_UOMCD] [nvarchar](max) NULL,
	[VMPS_Id] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Supplier_History]    Script Date: 07/14/2018 21:15:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Supplier_History](
	[SupplierHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[CDLive] [nvarchar](max) NULL,
	[DESCLive] [nvarchar](max) NULL,
	[CDChangeset] [nvarchar](max) NULL,
	[DESCChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_SupplierHistoryId] PRIMARY KEY CLUSTERED 
(
	[SupplierHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Supplier_Changeset_Backup]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Supplier_Changeset_Backup](
	[SupplierChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_SupplierChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[SupplierChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Supplier_Changeset]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Supplier_Changeset](
	[SupplierChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_SupplierChangesetId] PRIMARY KEY CLUSTERED 
(
	[SupplierChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Supplier]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Supplier]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Supplier](
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Route_History]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Route_History](
	[RouteHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[CDLive] [nvarchar](max) NULL,
	[DESCLive] [nvarchar](max) NULL,
	[CDChangeset] [nvarchar](max) NULL,
	[DESCChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_RouteHistoryId] PRIMARY KEY CLUSTERED 
(
	[RouteHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Route_Changeset_Backup]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Route_Changeset_Backup](
	[RouteChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_RouteChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[RouteChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Route_Changeset]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Route_Changeset](
	[RouteChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_RouteChangesetId] PRIMARY KEY CLUSTERED 
(
	[RouteChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Route]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Route]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Route](
	[CD] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Manufacturer]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Manufacturer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Manufacturer](
	[CD] [bigint] NOT NULL,
	[CDDT] [datetime] NULL,
	[CDPREV] [decimal](28, 0) NULL,
	[INVALID] [decimal](28, 0) NULL,
	[DESC] [nvarchar](255) NULL,
	[SUPPLIER_Id] [numeric](20, 0) NULL,
 CONSTRAINT [PK_Dmd_Manufacturer] PRIMARY KEY CLUSTERED 
(
	[CD] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Link_GtinSequence]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_GtinSequence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Link_GtinSequence](
	[GTINDATA_Id] [numeric](20, 0) NULL,
	[sequence_1_Id] [numeric](20, 0) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Link_GtinDetail]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_GtinDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Link_GtinDetail](
	[GTIN] [nvarchar](255) NULL,
	[STARTDT] [datetime] NULL,
	[ENDDT] [datetime] NULL,
	[GTINDATA_Id] [numeric](20, 0) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Link_AmppSequence]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Link_AmppSequence]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Link_AmppSequence](
	[sequence_1_Id] [numeric](20, 0) NULL,
	[AMPPID] [decimal](28, 10) NULL,
	[AMPP_Id] [numeric](20, 0) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Gtin_History]    Script Date: 07/14/2018 21:15:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Gtin_History](
	[GtinHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[AMPPIDLive] [nvarchar](max) NULL,
	[GTINDATAGTINLive] [nvarchar](max) NULL,
	[GTINDATASTARTDTLive] [nvarchar](max) NULL,
	[AMPPIDChangeset] [nvarchar](max) NULL,
	[GTINDATAGTINChangeset] [nvarchar](max) NULL,
	[GTINDATASTARTDTChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_GtinHistoryId] PRIMARY KEY CLUSTERED 
(
	[GtinHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Gtin_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Gtin_Changeset_Backup](
	[GtinChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[AMPPID] [nvarchar](max) NULL,
	[GTINDATAGTIN] [nvarchar](max) NULL,
	[GTINDATASTARTDT] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_GtinChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[GtinChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Gtin_Changeset]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Gtin_Changeset](
	[GtinChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[AMPPID] [nvarchar](max) NULL,
	[GTINDATAGTIN] [nvarchar](max) NULL,
	[GTINDATASTARTDT] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_GtinChangesetId] PRIMARY KEY CLUSTERED 
(
	[GtinChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Gtin]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Gtin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Gtin](
	[AMPPID] [nvarchar](max) NULL,
	[GTINDATAGTIN] [nvarchar](max) NULL,
	[GTINDATASTARTDT] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_FTPFileDownloadDetails]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_FTPFileDownloadDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_FTPFileDownloadDetails](
	[FTPFileDownloadID] [int] IDENTITY(1,1) NOT NULL,
	[ChagetsetName] [varchar](50) NULL,
	[DirectoryCreatedOn] [datetime] NULL,
	[CreatedOn] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Dmd_FTPFileDownloadDetails] PRIMARY KEY CLUSTERED 
(
	[FTPFileDownloadID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Form_History]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Form_History](
	[FormHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[CDLive] [nvarchar](max) NULL,
	[CDDTLive] [nvarchar](max) NULL,
	[CDPREVLive] [nvarchar](max) NULL,
	[DESCLive] [nvarchar](max) NULL,
	[CDChangeset] [nvarchar](max) NULL,
	[CDDTChangeset] [nvarchar](max) NULL,
	[CDPREVChangeset] [nvarchar](max) NULL,
	[DESCChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_FormHistoryId] PRIMARY KEY CLUSTERED 
(
	[FormHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Form_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Form_Changeset_Backup](
	[FormChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[CDDT] [nvarchar](max) NULL,
	[CDPREV] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_FormChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[FormChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Form_Changeset]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Form_Changeset](
	[FormChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[CD] [nvarchar](max) NULL,
	[CDDT] [nvarchar](max) NULL,
	[CDPREV] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[ChangsetId] [int] NULL,
	[ChangesetDate] [datetime] NULL,
 CONSTRAINT [PK_Dmd_FormChangesetId] PRIMARY KEY CLUSTERED 
(
	[FormChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Form]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Form]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Form](
	[CD] [nvarchar](max) NULL,
	[CDDT] [nvarchar](max) NULL,
	[CDPREV] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_DosageForm]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_DosageForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_DosageForm](
	[CD] [bigint] NOT NULL,
	[CDDT] [datetime] NULL,
	[CDPREV] [decimal](28, 0) NULL,
	[DESC] [nvarchar](255) NULL,
	[FORM_Id] [numeric](20, 0) NULL,
 CONSTRAINT [PK_Dmd_DosageForm] PRIMARY KEY CLUSTERED 
(
	[CD] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_ChangeSetDetails]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_ChangeSetDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_ChangeSetDetails](
	[DmdChangeSetDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[WeekNo] [varchar](50) NULL,
	[YearNo] [varchar](50) NULL,
	[ChangesetFileSize] [decimal](9, 2) NULL,
	[Isdeleted] [bit] NULL DEFAULT ((0)),
	[CreatedOn] [datetime] NULL DEFAULT (getdate()),
	[Isprocessed] [bit] NULL DEFAULT ((0)),
 CONSTRAINT [PK_Dmd_ChangeSetDetail] PRIMARY KEY CLUSTERED 
(
	[DmdChangeSetDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_BusinessChangeSetDetails]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_BusinessChangeSetDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_BusinessChangeSetDetails](
	[DmdBusinessChangeSetDetailID] [int] IDENTITY(1,1) NOT NULL,
	[FromDateChangeset] [varchar](50) NULL,
	[ToDateChangeset] [varchar](50) NULL,
	[FromDateChangesetId] [int] NULL,
	[ToDateChangesetId] [int] NULL,
	[CreatedOn] [datetime] NULL DEFAULT (getdate()),
	[BusinessEmail] [varchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Dmd_BusinessChangeSetDetail] PRIMARY KEY CLUSTERED 
(
	[DmdBusinessChangeSetDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Ampp_History]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Ampp_History](
	[AmppHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [varchar](50) NULL,
	[APPIDLive] [nvarchar](max) NULL,
	[INVALIDLive] [nvarchar](max) NULL,
	[NMLive] [nvarchar](max) NULL,
	[ABBREVNMLive] [nvarchar](max) NULL,
	[VPPIDLive] [nvarchar](max) NULL,
	[APIDLive] [nvarchar](max) NULL,
	[COMBPACKCDLive] [nvarchar](max) NULL,
	[LEGAL_CATCDLive] [nvarchar](max) NULL,
	[SUBPLive] [nvarchar](max) NULL,
	[DISCCDLive] [nvarchar](max) NULL,
	[DISCDTLive] [nvarchar](max) NULL,
	[AMPPS_IdLive] [nvarchar](max) NULL,
	[APPIDChangeset] [nvarchar](max) NULL,
	[INVALIDChangeset] [nvarchar](max) NULL,
	[NMChangeset] [nvarchar](max) NULL,
	[ABBREVNMChangeset] [nvarchar](max) NULL,
	[VPPIDChangeset] [nvarchar](max) NULL,
	[APIDChangeset] [nvarchar](max) NULL,
	[COMBPACKCDChangeset] [nvarchar](max) NULL,
	[LEGAL_CATCDChangeset] [nvarchar](max) NULL,
	[SUBPChangeset] [nvarchar](max) NULL,
	[DISCCDChangeset] [nvarchar](max) NULL,
	[DISCDTChangeset] [nvarchar](max) NULL,
	[AMPPS_IdChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_AmppHistoryId] PRIMARY KEY CLUSTERED 
(
	[AmppHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dmd_Ampp_Changeset_Backup]    Script Date: 07/14/2018 21:15:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Ampp_Changeset_Backup](
	[AmppChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[APPID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPPID] [nvarchar](max) NULL,
	[APID] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[LEGAL_CATCD] [nvarchar](max) NULL,
	[SUBP] [nvarchar](max) NULL,
	[DISCCD] [nvarchar](max) NULL,
	[DISCDT] [nvarchar](max) NULL,
	[AMPPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_AmppChangeseBackupId] PRIMARY KEY CLUSTERED 
(
	[AmppChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Ampp_Changeset]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Ampp_Changeset](
	[AmppChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[APPID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPPID] [nvarchar](max) NULL,
	[APID] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[LEGAL_CATCD] [nvarchar](max) NULL,
	[SUBP] [nvarchar](max) NULL,
	[DISCCD] [nvarchar](max) NULL,
	[DISCDT] [nvarchar](max) NULL,
	[AMPPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [datetime] NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_AmppChangesetId] PRIMARY KEY CLUSTERED 
(
	[AmppChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Ampp]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Ampp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Ampp](
	[APPID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[VPPID] [nvarchar](max) NULL,
	[APID] [nvarchar](max) NULL,
	[COMBPACKCD] [nvarchar](max) NULL,
	[LEGAL_CATCD] [nvarchar](max) NULL,
	[SUBP] [nvarchar](max) NULL,
	[DISCCD] [nvarchar](max) NULL,
	[DISCDT] [nvarchar](max) NULL,
	[AMPPS_Id] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Amp_History]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_History]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Amp_History](
	[AmpHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[ActionType] [nvarchar](max) NULL,
	[APIDLive] [nvarchar](max) NULL,
	[INVALIDLive] [nvarchar](max) NULL,
	[VPIDLive] [nvarchar](max) NULL,
	[NMLive] [nvarchar](max) NULL,
	[ABBREVNMLive] [nvarchar](max) NULL,
	[DESCLive] [nvarchar](max) NULL,
	[NMDTLive] [nvarchar](max) NULL,
	[NM_PREVLive] [nvarchar](max) NULL,
	[SUPPCDLive] [nvarchar](max) NULL,
	[LIC_AUTHCDLive] [nvarchar](max) NULL,
	[LIC_AUTH_PREVCDLive] [nvarchar](max) NULL,
	[LIC_AUTHCHANGECDLive] [nvarchar](max) NULL,
	[LIC_AUTHCHANGEDTLive] [nvarchar](max) NULL,
	[COMBPRODCDLive] [nvarchar](max) NULL,
	[FLAVOURCDLive] [nvarchar](max) NULL,
	[EMALive] [nvarchar](max) NULL,
	[PARALLEL_IMPORTLive] [nvarchar](max) NULL,
	[AVAIL_RESTRICTCDLive] [nvarchar](max) NULL,
	[AMPS_IdLive] [nvarchar](max) NULL,
	[APIDChangeset] [nvarchar](max) NULL,
	[INVALIDChangeset] [nvarchar](max) NULL,
	[VPIDChangeset] [nvarchar](max) NULL,
	[NMChangeset] [nvarchar](max) NULL,
	[ABBREVNMChangeset] [nvarchar](max) NULL,
	[DESCChangeset] [nvarchar](max) NULL,
	[NMDTChangeset] [nvarchar](max) NULL,
	[NM_PREVChangeset] [nvarchar](max) NULL,
	[SUPPCDChangeset] [nvarchar](max) NULL,
	[LIC_AUTHCDChangeset] [nvarchar](max) NULL,
	[LIC_AUTH_PREVCDChangeset] [nvarchar](max) NULL,
	[LIC_AUTHCHANGECDChangeset] [nvarchar](max) NULL,
	[LIC_AUTHCHANGEDTChangeset] [nvarchar](max) NULL,
	[COMBPRODCDChangeset] [nvarchar](max) NULL,
	[FLAVOURCDChangeset] [nvarchar](max) NULL,
	[EMAChangeset] [nvarchar](max) NULL,
	[PARALLEL_IMPORTChangeset] [nvarchar](max) NULL,
	[AVAIL_RESTRICTCDChangeset] [nvarchar](max) NULL,
	[AMPS_IdChangeset] [nvarchar](max) NULL,
	[DmdChangeSetDetailID] [int] NULL,
 CONSTRAINT [PK_Dmd_AmpHistoryId] PRIMARY KEY CLUSTERED 
(
	[AmpHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Amp_Changeset_Backup]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_Changeset_Backup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Amp_Changeset_Backup](
	[AmpChangesetBackupId] [int] IDENTITY(1,1) NOT NULL,
	[APID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NM_PREV] [nvarchar](max) NULL,
	[SUPPCD] [nvarchar](max) NULL,
	[LIC_AUTHCD] [nvarchar](max) NULL,
	[LIC_AUTH_PREVCD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGECD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGEDT] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[FLAVOURCD] [nvarchar](max) NULL,
	[EMA] [nvarchar](max) NULL,
	[PARALLEL_IMPORT] [nvarchar](max) NULL,
	[AVAIL_RESTRICTCD] [nvarchar](max) NULL,
	[AMPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_AmpChangesetBackupId] PRIMARY KEY CLUSTERED 
(
	[AmpChangesetBackupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Amp_Changeset]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp_Changeset]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Amp_Changeset](
	[AmpChangesetId] [int] IDENTITY(1,1) NOT NULL,
	[APID] [nvarchar](max) NULL,
	[INVALID] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NM_PREV] [nvarchar](max) NULL,
	[SUPPCD] [nvarchar](max) NULL,
	[LIC_AUTHCD] [nvarchar](max) NULL,
	[LIC_AUTH_PREVCD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGECD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGEDT] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[FLAVOURCD] [nvarchar](max) NULL,
	[EMA] [nvarchar](max) NULL,
	[PARALLEL_IMPORT] [nvarchar](max) NULL,
	[AVAIL_RESTRICTCD] [nvarchar](max) NULL,
	[AMPS_Id] [nvarchar](max) NULL,
	[ChangsetId] [bigint] NULL,
	[ChangesetDate] [nvarchar](max) NULL,
	[IsAdded] [bit] NULL,
	[IsUpdated] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Dmd_AmpChangesetId] PRIMARY KEY CLUSTERED 
(
	[AmpChangesetId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_Amp]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_Amp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_Amp](
	[APID] [nvarchar](max) NOT NULL,
	[INVALID] [nvarchar](max) NULL,
	[VPID] [nvarchar](max) NULL,
	[NM] [nvarchar](max) NULL,
	[ABBREVNM] [nvarchar](max) NULL,
	[DESC] [nvarchar](max) NULL,
	[NMDT] [nvarchar](max) NULL,
	[NM_PREV] [nvarchar](max) NULL,
	[SUPPCD] [nvarchar](max) NULL,
	[LIC_AUTHCD] [nvarchar](max) NULL,
	[LIC_AUTH_PREVCD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGECD] [nvarchar](max) NULL,
	[LIC_AUTHCHANGEDT] [nvarchar](max) NULL,
	[COMBPRODCD] [nvarchar](max) NULL,
	[FLAVOURCD] [nvarchar](max) NULL,
	[EMA] [nvarchar](max) NULL,
	[PARALLEL_IMPORT] [nvarchar](max) NULL,
	[AVAIL_RESTRICTCD] [nvarchar](max) NULL,
	[AMPS_Id] [nvarchar](max) NULL
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Dmd_AdministrationMethod]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Dmd_AdministrationMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Dmd_AdministrationMethod](
	[CD] [bigint] NOT NULL,
	[CDDT] [datetime] NULL,
	[CDPREV] [decimal](28, 0) NULL,
	[DESC] [nvarchar](255) NULL,
	[ROUTE_Id] [numeric](20, 0) NULL,
 CONSTRAINT [PK_Dmd_AdministrationMethod] PRIMARY KEY CLUSTERED 
(
	[CD] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[CustomerTB]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerTB]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CustomerTB](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Address] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[Phoneno] [varchar](10) NULL,
 CONSTRAINT [PK_CustomerTB] PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contacts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contacts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[ContactType] [nvarchar](100) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
	[IsArchived] [bit] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[Phone1] [nvarchar](50) NULL,
	[Phone2] [nvarchar](50) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BusinessUser]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessUser]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessUser](
	[BusinessUserId] [int] IDENTITY(1,1) NOT NULL,
	[BusinessId] [int] NULL,
	[IdentityUserId] [nvarchar](450) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_BusinessUser_BusinessUserId] PRIMARY KEY CLUSTERED 
(
	[BusinessUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[BusinessDetails]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessDetails]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BusinessName] [varchar](100) NULL,
	[LastSubscribedDate] [datetime] NULL,
	[LastExpirationDate] [datetime] NULL,
	[AddressLine1] [nvarchar](max) NULL,
	[AddressLine2] [varchar](500) NULL,
	[AddressLine3] [nvarchar](500) NULL,
	[City] [varchar](500) NULL,
	[Postcode] [varchar](500) NULL,
	[ContactPerson] [varchar](500) NULL,
	[ContactEmail] [varchar](500) NULL,
	[ContactPhone] [varchar](500) NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[IdentityUserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_BusinessDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 07/14/2018 21:15:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[GetDmdReports]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDmdReports]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetDmdReports] 
	-- Add the parameters for the stored procedure here
	@ReportType varchar(50),
	@ChangeSetVesrionIDFrom int,
	@ChangeSetVesrionIDTo int
AS
BEGIN
	select * from Dmd_Amp_History
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[DmdProcessXMLFileToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DmdProcessXMLFileToChangeset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCedure [dbo].[DmdProcessXMLFileToChangeset]
AS
BEGIN

DECLARE @TotalRowCount int,
@CurrentRow int =1 ,
@FilePath varchar(max),
@sql nvarchar(max),
@outputxml xml,
@TableName varchar(50)

SET @TotalRowCount =(Select Count(*) from UploadedFiles (NOLOCK) where Isprocessed=0 and Isdeleted=0 and 
cast(createdon as date) =cast(getdate() as date))
While(@CurrentRow <= @TotalRowCount)

BEGIN

SELECT @FilePath  = FilePath,
	   @TableName =TableName FROM UploadedFiles WHERE UploadedFileID = @CurrentRow AND 
	   Isprocessed=0 AND
	   Isdeleted=0 AND 
      cast(createdon as date) =cast(getdate() as date)


 select @sql = ''select @xmlOut = x.BulkColumn from openrowset(
     bulk '''''' + @FilePath + '''''', single_blob) x'';
 
 exec sp_executesql @sql, N''@xmlOut xml out'', @xmlOut = @outputxml out;

 update UploadedFiles set XmlData=@outputxml WHERE UploadedFileID = @CurrentRow 

 --select @outputxml
 print ''DmdProcessXMLToStaging # '' + convert(varchar(10),@TableName)
 exec DmdProcessXMLToStaging @outputxml,@TableName

print ''Current ID # '' + convert(varchar(10),@CurrentRow)

SET  @CurrentRow =@CurrentRow +1  
   
END
END


' 
END
GO
/****** Object:  Table [dbo].[AdministrationMethod]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdministrationMethod](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DmdRouteId] [bigint] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Status_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AdministrationMethod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Manufacturer]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Manufacturer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DmdSupplierId] [bigint] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Status_Id] [int] NULL,
 CONSTRAINT [PK_dbo.Manufacturer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[ProcessDmdDataToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProcessDmdDataToChangeset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCedure [dbo].[ProcessDmdDataToChangeset]
AS
BEGIN

DECLARE @TotalRowCount int,
@CurrentRow int =1 ,
@FilePath varchar(max),
@sql nvarchar(max),
@outputxml xml,
@TableName varchar(50)

SET @TotalRowCount =(Select Count(*) from UploadedFiles)
While(@CurrentRow <= @TotalRowCount)

BEGIN

SELECT @FilePath  = FilePath,
	   @TableName =TableName FROM UploadedFiles WHERE UploadedFileID = @CurrentRow 


 select @sql = ''select @xmlOut = x.BulkColumn from openrowset(
     bulk '''''' + @FilePath + '''''', single_blob) x'';
 
 exec sp_executesql @sql, N''@xmlOut xml out'', @xmlOut = @outputxml out;

 update UploadedFiles set XmlData=@outputxml WHERE UploadedFileID = @CurrentRow 

 --select @outputxml
 print ''DmdProcessXMLToStaging # '' + convert(varchar(10),@TableName)
 exec DmdProcessXMLToStaging @outputxml,@TableName

print ''Current ID # '' + convert(varchar(10),@CurrentRow)

SET  @CurrentRow =@CurrentRow +1  
   
END
END

' 
END
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 07/14/2018 21:15:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Addresses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Addresses](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Address1] [nvarchar](250) NULL,
	[Address2] [nvarchar](250) NULL,
	[AddressTypeId] [int] NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[City] [nvarchar](100) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
	[State] [nvarchar](100) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[Zip] [nvarchar](100) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DosageFormType]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DosageFormType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DosageFormType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[Status_Id] [int] NULL,
 CONSTRAINT [PK_dbo.DosageFormType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserModule]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserModule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserModule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[UserId] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserModule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessStagingToHistory]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessStagingToHistory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'











CREATE PROCEDURE [dbo].[SP_DmdProcessStagingToHistory]
       -- Add the parameters for the stored procedure here
       @DmdChangeSetDetailID int	 
	   
AS
BEGIN


----------------------------------------- 01.Dmd_Vtm --------------------------------------------------------------------
MERGE Dmd_Vtm AS TARGET
USING Dmd_Vtm_Changeset AS SOURCE 
ON (TARGET.VTMID = SOURCE.VTMID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 
ISNULL(TARGET.INVALID,'''') <> ISNULL(SOURCE.INVALID,'''') OR 
ISNULL(TARGET.ABBREVNM,'''') <> ISNULL(SOURCE.ABBREVNM,'''') OR
ISNULL(TARGET.NM,'''') <> ISNULL(SOURCE.NM,'''') OR
ISNULL(TARGET.VTMIDPREV,'''') <> ISNULL(SOURCE.VTMIDPREV,'''') OR
ISNULL(TARGET.VTMIDDT,'''') <> ISNULL(SOURCE.VTMIDDT,'''')

THEN 
UPDATE SET 
TARGET.INVALID = SOURCE.INVALID, 
TARGET.NM = SOURCE.NM,
TARGET.ABBREVNM = SOURCE.ABBREVNM,
TARGET.VTMIDPREV = SOURCE.VTMIDPREV, 
TARGET.VTMIDDT = SOURCE.VTMIDDT

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (VTMID,INVALID,NM,ABBREVNM,VTMIDPREV,VTMIDDT) 
VALUES (SOURCE.VTMID, SOURCE.INVALID, SOURCE.NM,SOURCE.ABBREVNM,SOURCE.VTMIDPREV,SOURCE.VTMIDDT)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,  
DELETED.VTMID AS VTMIDLive, 
DELETED.INVALID AS INVALIDLive, 
DELETED.NM AS NMLive, 
DELETED.ABBREVNM AS ABBREVNMLive, 
DELETED.VTMIDPREV AS VTMIDPREVLive, 
DELETED.VTMIDDT AS VTMIDDTLive,

INSERTED.VTMID AS VTMIDChangeset, 
INSERTED.INVALID AS INVALIDChangeset, 
INSERTED.NM AS NMChangeset, 
INSERTED.ABBREVNM AS ABBREVNMChangeset, 
INSERTED.VTMIDPREV AS VTMIDPREVChangeset, 
INSERTED.VTMIDDT AS VTMIDDTChangeset,
@DmdChangeSetDetailID

INTO Dmd_Vtm_History;

INSERT INTO Dmd_Vtm_Changeset_Backup (VTMID,INVALID,NM,ABBREVNM,VTMIDPREV,VTMIDDT,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted)
SELECT VTMID,INVALID,NM,ABBREVNM,VTMIDPREV,VTMIDDT,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted FROM Dmd_Vtm_Changeset

TRUNCATE TABLE Dmd_Vtm_Changeset
----------------------------------------- 02.Dmd_Vmpp --------------------------------------------------------------------

MERGE Dmd_Vmpp AS TARGET
USING Dmd_Vmpp_Changeset AS SOURCE 
ON (TARGET.VPPID = SOURCE.VPPID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 
ISNULL(TARGET.INVALID,'''') <> ISNULL(SOURCE.INVALID,'''') OR 
ISNULL(TARGET.NM,'''') <> ISNULL(SOURCE.NM,'''') OR
ISNULL(TARGET.ABBREVNM,'''') <> ISNULL(SOURCE.ABBREVNM,'''') OR
ISNULL(TARGET.VPID,'''') <> ISNULL(SOURCE.VPID,'''') OR
ISNULL(TARGET.QTYVAL,'''') <> ISNULL(SOURCE.QTYVAL,'''') OR
ISNULL(TARGET.QTY_UOMCD,'''') <> ISNULL(SOURCE.QTY_UOMCD,'''') OR
ISNULL(TARGET.COMBPACKCD,'''') <> ISNULL(SOURCE.VMPPS_Id,'''') OR
ISNULL(TARGET.VMPPS_Id,'''') <> ISNULL(SOURCE.COMBPACKCD,'''') 

THEN 
UPDATE SET 
TARGET.INVALID = SOURCE.INVALID, 
TARGET.NM = SOURCE.NM,
TARGET.ABBREVNM = SOURCE.ABBREVNM,
TARGET.VPID = SOURCE.VPID, 
TARGET.QTYVAL = SOURCE.QTYVAL,
TARGET.QTY_UOMCD = SOURCE.QTY_UOMCD,
TARGET.COMBPACKCD = SOURCE.COMBPACKCD,
TARGET.VMPPS_Id = SOURCE.VMPPS_Id

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id) 
VALUES (SOURCE.VPPID, SOURCE.INVALID, SOURCE.NM,SOURCE.ABBREVNM,SOURCE.VPID,SOURCE.QTYVAL,SOURCE.QTY_UOMCD,SOURCE.COMBPACKCD,SOURCE.VMPPS_Id)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType, 
Deleted.VPPID  AS  VPPIDLive,
Deleted.INVALID  AS  INVALIDLive,
Deleted.NM  AS  NMLive,
Deleted.ABBREVNM  AS  ABBREVNMLive,
Deleted.VPID  AS  VPIDLive,
Deleted.QTYVAL  AS  QTYVALLive,
Deleted.QTY_UOMCD  AS  QTY_UOMCDLive,
Deleted.COMBPACKCD  AS  COMBPACKCDLive,
Deleted.VMPPS_Id  AS  VMPPS_IdLive,

INSERTED.VPPID  AS  VPPIDLive,
INSERTED.INVALID  AS  INVALIDLive,
INSERTED.NM  AS  NMLive,
INSERTED.ABBREVNM  AS  ABBREVNMLive,
INSERTED.VPID  AS  VPIDLive,
INSERTED.QTYVAL  AS  QTYVALLive,
INSERTED.QTY_UOMCD  AS  QTY_UOMCDLive,
INSERTED.COMBPACKCD  AS  COMBPACKCDLive,
INSERTED.VMPPS_Id  AS  VMPPS_IdLive,
@DmdChangeSetDetailID
INTO Dmd_Vmpp_History;

INSERT INTO [Dmd_Vmpp_Changeset_Backup]
SELECT VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted FROM Dmd_Vmpp_Changeset

TRUNCATE TABLE Dmd_Vmpp_Changeset

----------------------------------------- 03.Dmd_Vmp_History --------------------------------------------------------------------

MERGE Dmd_Vmp AS TARGET
USING Dmd_Vmp_Changeset AS SOURCE 
ON (TARGET.VPID = SOURCE.VPID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.VPIDDT,'''') <> ISNULL(SOURCE.VPIDDT,'''') OR
ISNULL(TARGET.VPIDPREV,'''') <> ISNULL(SOURCE.VPIDPREV,'''') OR
ISNULL(TARGET.VTMID,'''') <> ISNULL(SOURCE.VTMID,'''') OR
ISNULL(TARGET.INVALID,'''') <> ISNULL(SOURCE.INVALID,'''') OR
ISNULL(TARGET.NM,'''') <> ISNULL(SOURCE.NM,'''') OR
ISNULL(TARGET.ABBREVNM,'''') <> ISNULL(SOURCE.ABBREVNM,'''') OR
ISNULL(TARGET.BASISCD,'''') <> ISNULL(SOURCE.BASISCD,'''') OR
ISNULL(TARGET.NMDT,'''') <> ISNULL(SOURCE.NMDT,'''') OR
ISNULL(TARGET.NMPREV,'''') <> ISNULL(SOURCE.NMPREV,'''') OR
ISNULL(TARGET.BASIS_PREVCD,'''') <> ISNULL(SOURCE.BASIS_PREVCD,'''') OR
ISNULL(TARGET.NMCHANGECD,'''') <> ISNULL(SOURCE.NMCHANGECD,'''') OR
ISNULL(TARGET.COMBPRODCD,'''') <> ISNULL(SOURCE.COMBPRODCD,'''') OR
ISNULL(TARGET.PRES_STATCD,'''') <> ISNULL(SOURCE.PRES_STATCD,'''') OR
ISNULL(TARGET.SUG_F,'''') <> ISNULL(SOURCE.SUG_F,'''') OR
ISNULL(TARGET.GLU_F,'''') <> ISNULL(SOURCE.GLU_F,'''') OR
ISNULL(TARGET.PRES_F,'''') <> ISNULL(SOURCE.PRES_F,'''') OR
ISNULL(TARGET.CFC_F,'''') <> ISNULL(SOURCE.CFC_F,'''') OR
ISNULL(TARGET.NON_AVAILCD,'''') <> ISNULL(SOURCE.NON_AVAILCD,'''') OR
ISNULL(TARGET.NON_AVAILDT,'''') <> ISNULL(SOURCE.NON_AVAILDT,'''') OR
ISNULL(TARGET.DF_INDCD,'''') <> ISNULL(SOURCE.DF_INDCD,'''') OR
ISNULL(TARGET.UDFS,'''') <> ISNULL(SOURCE.UDFS,'''') OR
ISNULL(TARGET.UDFS_UOMCD,'''') <> ISNULL(SOURCE.UDFS_UOMCD,'''') OR
ISNULL(TARGET.UNIT_DOSE_UOMCD,'''') <> ISNULL(SOURCE.UNIT_DOSE_UOMCD,'''') OR
ISNULL(TARGET.VMPS_Id,'''') <> ISNULL(SOURCE.VMPS_Id,'''') 


THEN 
UPDATE SET 

TARGET.VPIDDT=SOURCE.VPIDDT,
TARGET.VPIDPREV=SOURCE.VPIDPREV,
TARGET.VTMID=SOURCE.VTMID,
TARGET.INVALID=SOURCE.INVALID,
TARGET.NM=SOURCE.NM,
TARGET.ABBREVNM=SOURCE.ABBREVNM,
TARGET.BASISCD=SOURCE.BASISCD,
TARGET.NMDT=SOURCE.NMDT,
TARGET.NMPREV=SOURCE.NMPREV,
TARGET.BASIS_PREVCD=SOURCE.BASIS_PREVCD,
TARGET.NMCHANGECD=SOURCE.NMCHANGECD,
TARGET.COMBPRODCD=SOURCE.COMBPRODCD,
TARGET.PRES_STATCD=SOURCE.PRES_STATCD,
TARGET.SUG_F=SOURCE.SUG_F,
TARGET.GLU_F=SOURCE.GLU_F,
TARGET.PRES_F=SOURCE.PRES_F,
TARGET.CFC_F=SOURCE.CFC_F,
TARGET.NON_AVAILCD=SOURCE.NON_AVAILCD,
TARGET.NON_AVAILDT=SOURCE.NON_AVAILDT,
TARGET.DF_INDCD=SOURCE.DF_INDCD,
TARGET.UDFS=SOURCE.UDFS,
TARGET.UDFS_UOMCD=SOURCE.UDFS_UOMCD,
TARGET.UNIT_DOSE_UOMCD=SOURCE.UNIT_DOSE_UOMCD,
TARGET.VMPS_Id=SOURCE.VMPS_Id


--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,
          UDFS,UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id) 
VALUES (SOURCE.VPID,SOURCE.VPIDDT,SOURCE.VPIDPREV,SOURCE.VTMID,SOURCE.INVALID,SOURCE.NM,SOURCE.ABBREVNM,SOURCE.BASISCD,SOURCE.NMDT,SOURCE.NMPREV,SOURCE.BASIS_PREVCD,
SOURCE.NMCHANGECD,SOURCE.COMBPRODCD,SOURCE.PRES_STATCD,SOURCE.SUG_F,SOURCE.GLU_F,SOURCE.PRES_F,SOURCE.CFC_F,SOURCE.NON_AVAILCD,SOURCE.NON_AVAILDT,
SOURCE.DF_INDCD,SOURCE.UDFS,SOURCE.UDFS_UOMCD,SOURCE.UNIT_DOSE_UOMCD,SOURCE.VMPS_Id)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType, 
Deleted.VPID  AS  VPIDLive,
Deleted.VPIDDT  AS  VPIDDTLive,
Deleted.VPIDPREV  AS  VPIDPREVLive,
Deleted.VTMID  AS  VTMIDLive,
Deleted.INVALID  AS  INVALIDLive,
Deleted.NM  AS  NMLive,
Deleted.ABBREVNM  AS  ABBREVNMLive,
Deleted.BASISCD  AS  BASISCDLive,
Deleted.NMDT  AS  NMDTLive,
Deleted.NMPREV  AS  NMPREVLive,
Deleted.BASIS_PREVCD  AS  BASIS_PREVCDLive,
Deleted.NMCHANGECD  AS  NMCHANGECDLive,
Deleted.COMBPRODCD  AS  COMBPRODCDLive,
Deleted.PRES_STATCD  AS  PRES_STATCDLive,
Deleted.SUG_F  AS  SUG_FLive,
Deleted.GLU_F  AS  GLU_FLive,
Deleted.PRES_F  AS  PRES_FLive,
Deleted.CFC_F  AS  CFC_FLive,
Deleted.NON_AVAILCD  AS  NON_AVAILCDLive,
Deleted.NON_AVAILDT  AS  NON_AVAILDTLive,
Deleted.DF_INDCD  AS  DF_INDCDLive,
Deleted.UDFS  AS  UDFSLive,
Deleted.UDFS_UOMCD  AS  UDFS_UOMCDLive,
Deleted.UNIT_DOSE_UOMCD  AS  UNIT_DOSE_UOMCDLive,
Deleted.VMPS_Id  AS  VMPS_IdLive,


INSERTED.VPID  AS  VPIDChangeset,
INSERTED.VPIDDT  AS  VPIDDTChangeset,
INSERTED.VPIDPREV  AS  VPIDPREVChangeset,
INSERTED.VTMID  AS  VTMIDChangeset,
INSERTED.INVALID  AS  INVALIDChangeset,
INSERTED.NM  AS  NMChangeset,
INSERTED.ABBREVNM  AS  ABBREVNMChangeset,
INSERTED.BASISCD  AS  BASISCDChangeset,
INSERTED.NMDT  AS  NMDTChangeset,
INSERTED.NMPREV  AS  NMPREVChangeset,
INSERTED.BASIS_PREVCD  AS  BASIS_PREVCDChangeset,
INSERTED.NMCHANGECD  AS  NMCHANGECDChangeset,
INSERTED.COMBPRODCD  AS  COMBPRODCDChangeset,
INSERTED.PRES_STATCD  AS  PRES_STATCDChangeset,
INSERTED.SUG_F  AS  SUG_FChangeset,
INSERTED.GLU_F  AS  GLU_FChangeset,
INSERTED.PRES_F  AS  PRES_FChangeset,
INSERTED.CFC_F  AS  CFC_FChangeset,
INSERTED.NON_AVAILCD  AS  NON_AVAILCDChangeset,
INSERTED.NON_AVAILDT  AS  NON_AVAILDTChangeset,
INSERTED.DF_INDCD  AS  DF_INDCDChangeset,
INSERTED.UDFS  AS  UDFSChangeset,
INSERTED.UDFS_UOMCD  AS  UDFS_UOMCDChangeset,
INSERTED.UNIT_DOSE_UOMCD  AS  UNIT_DOSE_UOMCDChangeset,
INSERTED.VMPS_Id  AS  VMPS_IdChangeset,
@DmdChangeSetDetailID
INTO Dmd_Vmp_History;


INSERT INTO [Dmd_Vmp_Changeset_Backup]
SELECT VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,UDFS,
UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted FROM Dmd_Vmp_Changeset
TRUNCATE TABLE Dmd_Vmp_Changeset
----------------------------------------- 04.Dmd_Ampp --------------------------------------------------------------------

MERGE Dmd_Ampp AS TARGET
USING Dmd_Ampp_Changeset AS SOURCE 
ON (TARGET.APPID = SOURCE.APPID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.INVALID,'''') <> ISNULL(SOURCE.INVALID,'''') OR
ISNULL(TARGET.NM,'''') <> ISNULL(SOURCE.NM,'''') OR
ISNULL(TARGET.[ABBREVNM],'''') <> ISNULL(SOURCE.[ABBREVNM],'''') OR
ISNULL(TARGET.VPPID	,'''') <> ISNULL(SOURCE.VPPID,'''') OR
ISNULL(TARGET.APID,'''') <> ISNULL(SOURCE.APID,'''') OR
ISNULL(TARGET.COMBPACKCD,'''') <> ISNULL(SOURCE.COMBPACKCD,'''') OR
ISNULL(TARGET.LEGAL_CATCD,'''') <> ISNULL(SOURCE.LEGAL_CATCD,'''') OR
ISNULL(TARGET.SUBP,'''') <> ISNULL(SOURCE.SUBP,'''') OR
ISNULL(TARGET.DISCCD,'''') <> ISNULL(SOURCE.DISCCD,'''') OR
ISNULL(TARGET.DISCDT,'''') <> ISNULL(SOURCE.DISCDT,'''') OR
ISNULL(TARGET.AMPPS_Id,'''') <> ISNULL(SOURCE.AMPPS_Id,'''') 

THEN 
UPDATE SET 

TARGET.INVALID=SOURCE.INVALID,
TARGET.NM=SOURCE.NM,
TARGET.ABBREVNM=SOURCE.ABBREVNM,
TARGET.VPPID=SOURCE.VPPID,
TARGET.APID=SOURCE.APID,
TARGET.COMBPACKCD=SOURCE.COMBPACKCD,
TARGET.LEGAL_CATCD=SOURCE.LEGAL_CATCD,
TARGET.SUBP=SOURCE.SUBP,
TARGET.DISCCD=SOURCE.DISCCD,
TARGET.DISCDT=SOURCE.DISCDT,
TARGET.AMPPS_Id=SOURCE.AMPPS_Id



--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id) 
VALUES (SOURCE.APPID,SOURCE.INVALID,SOURCE.NM,SOURCE.ABBREVNM,SOURCE.VPPID,SOURCE.APID,SOURCE.COMBPACKCD,SOURCE.LEGAL_CATCD,SOURCE.SUBP,SOURCE.DISCCD,SOURCE.DISCDT,SOURCE.AMPPS_Id
)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType, 
Deleted.APPID  AS  APPIDLive,
Deleted.INVALID  AS  INVALIDLive,
Deleted.NM  AS  NMLive,
Deleted.ABBREVNM  AS  ABBREVNMLive,
Deleted.VPPID  AS  VPPIDLive,
Deleted.APID  AS  APIDLive,
Deleted.COMBPACKCD  AS  COMBPACKCDLive,
Deleted.LEGAL_CATCD  AS  LEGAL_CATCDLive,
Deleted.SUBP  AS  SUBPLive,
Deleted.DISCCD  AS  DISCCDLive,
Deleted.DISCDT  AS  DISCDTLive,
Deleted.AMPPS_Id  AS  AMPPS_IdLive,
INSERTED.APPID  AS  APPIDChangeset,
INSERTED.INVALID  AS  INVALIDChangeset,
INSERTED.NM  AS  NMChangeset,
INSERTED.ABBREVNM  AS  ABBREVNMChangeset,
INSERTED.VPPID  AS  VPPIDChangeset,
INSERTED.APID  AS  APIDChangeset,
INSERTED.COMBPACKCD  AS  COMBPACKCDChangeset,
INSERTED.LEGAL_CATCD  AS  LEGAL_CATCDChangeset,
INSERTED.SUBP  AS  SUBPChangeset,
INSERTED.DISCCD  AS  DISCCDChangeset,
INSERTED.DISCDT  AS  DISCDTChangeset,
INSERTED.AMPPS_Id  AS  AMPPS_IdChangeset,
@DmdChangeSetDetailID

INTO Dmd_Ampp_History;

INSERT INTO Dmd_Ampp_Changeset_Backup
SELECT APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted FROM Dmd_Ampp_Changeset
TRUNCATE TABLE Dmd_Ampp_Changeset

----------------------------------------- 05.Dmd_Amp --------------------------------------------------------------------


MERGE Dmd_Amp AS TARGET
USING Dmd_Amp_Changeset AS SOURCE 
ON (TARGET.APID = SOURCE.APID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.INVALID,'''') <> ISNULL(SOURCE.INVALID,'''') OR
ISNULL(TARGET.VPID,'''') <> ISNULL(SOURCE.VPID,'''') OR
ISNULL(TARGET.NM,'''') <> ISNULL(SOURCE.NM,'''') OR
ISNULL(TARGET.[DESC],'''') <> ISNULL(SOURCE.[DESC],'''') OR
ISNULL(TARGET.[ABBREVNM],'''') <> ISNULL(SOURCE.[ABBREVNM],'''') OR
ISNULL(TARGET.NMDT	,'''') <> ISNULL(SOURCE.NMDT,'''') OR
ISNULL(TARGET.NM_PREV,'''') <> ISNULL(SOURCE.NM_PREV,'''') OR
ISNULL(TARGET.SUPPCD,'''') <> ISNULL(SOURCE.SUPPCD,'''') OR
ISNULL(TARGET.LIC_AUTHCD,'''') <> ISNULL(SOURCE.LIC_AUTHCD,'''') OR
ISNULL(TARGET.LIC_AUTH_PREVCD,'''') <> ISNULL(SOURCE.LIC_AUTH_PREVCD,'''') OR
ISNULL(TARGET.LIC_AUTHCHANGECD,'''') <> ISNULL(SOURCE.LIC_AUTHCHANGECD,'''') OR
ISNULL(TARGET.LIC_AUTHCHANGEDT,'''') <> ISNULL(SOURCE.LIC_AUTHCHANGEDT,'''') OR
ISNULL(TARGET.COMBPRODCD,'''') <> ISNULL(SOURCE.COMBPRODCD,'''') OR
ISNULL(TARGET.FLAVOURCD,'''') <> ISNULL(SOURCE.FLAVOURCD,'''') OR
ISNULL(TARGET.EMA,'''') <> ISNULL(SOURCE.EMA,'''') OR
ISNULL(TARGET.PARALLEL_IMPORT,'''') <> ISNULL(SOURCE.PARALLEL_IMPORT,'''') OR
ISNULL(TARGET.AVAIL_RESTRICTCD,'''') <> ISNULL(SOURCE.AVAIL_RESTRICTCD,'''') OR
ISNULL(TARGET.AMPS_Id,'''') <> ISNULL(SOURCE.AMPS_Id,'''') 

THEN 
UPDATE SET 

TARGET.INVALID=SOURCE.INVALID,
TARGET.VPID=SOURCE.VPID,
TARGET.NM=SOURCE.NM,
TARGET.ABBREVNM=SOURCE.ABBREVNM,
TARGET.[DESC]=SOURCE.[DESC],
TARGET.NMDT=SOURCE.NMDT,
TARGET.NM_PREV=SOURCE.NM_PREV,
TARGET.SUPPCD=SOURCE.SUPPCD,
TARGET.LIC_AUTHCD=SOURCE.LIC_AUTHCD,
TARGET.LIC_AUTH_PREVCD=SOURCE.LIC_AUTH_PREVCD,
TARGET.LIC_AUTHCHANGECD=SOURCE.LIC_AUTHCHANGECD,
TARGET.LIC_AUTHCHANGEDT=SOURCE.LIC_AUTHCHANGEDT,
TARGET.COMBPRODCD=SOURCE.COMBPRODCD,
TARGET.FLAVOURCD=SOURCE.FLAVOURCD,
TARGET.EMA=SOURCE.EMA,
TARGET.PARALLEL_IMPORT=SOURCE.PARALLEL_IMPORT,
TARGET.AVAIL_RESTRICTCD=SOURCE.AVAIL_RESTRICTCD,
TARGET.AMPS_Id=SOURCE.AMPS_Id



--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,
        AMPS_Id) 
VALUES (SOURCE.APID,SOURCE.INVALID,SOURCE.VPID,SOURCE.NM,SOURCE.ABBREVNM,SOURCE.[DESC],SOURCE.NMDT,SOURCE.NM_PREV,SOURCE.SUPPCD,SOURCE.LIC_AUTHCD,SOURCE.LIC_AUTH_PREVCD,SOURCE.LIC_AUTHCHANGECD,
SOURCE.LIC_AUTHCHANGEDT,SOURCE.COMBPRODCD,SOURCE.FLAVOURCD,SOURCE.EMA,SOURCE.PARALLEL_IMPORT,SOURCE.AVAIL_RESTRICTCD,SOURCE.AMPS_Id)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,
Deleted.APID  AS  APIDLive,
Deleted.INVALID  AS  INVALIDLive,
Deleted.VPID  AS  VPIDLive,
Deleted.NM  AS  NMLive,
Deleted.ABBREVNM  AS  ABBREVNMLive,
Deleted.[DESC]  AS  DESCLive,
Deleted.NMDT  AS  NMDTLive,
Deleted.NM_PREV  AS  NM_PREVLive,
Deleted.SUPPCD  AS  SUPPCDLive,
Deleted.LIC_AUTHCD  AS  LIC_AUTHCDLive,
Deleted.LIC_AUTH_PREVCD  AS  LIC_AUTH_PREVCDLive,
Deleted.LIC_AUTHCHANGECD  AS  LIC_AUTHCHANGECDLive,
Deleted.LIC_AUTHCHANGEDT  AS  LIC_AUTHCHANGEDTLive,
Deleted.COMBPRODCD  AS  COMBPRODCDLive,
Deleted.FLAVOURCD  AS  FLAVOURCDLive,
Deleted.EMA  AS  EMALive,
Deleted.PARALLEL_IMPORT  AS  PARALLEL_IMPORTLive,
Deleted.AVAIL_RESTRICTCD  AS  AVAIL_RESTRICTCDLive,
Deleted.AMPS_Id  AS  AMPS_IdLive,
INSERTED.APID  AS  APIDChangeset,
INSERTED.INVALID  AS  INVALIDChangeset,
INSERTED.VPID  AS  VPIDChangeset,
INSERTED.NM  AS  NMChangeset,
INSERTED.ABBREVNM  AS  ABBREVNMChangeset,
INSERTED.[DESC]  AS  DESCChangeset,
INSERTED.NMDT  AS  NMDTChangeset,
INSERTED.NM_PREV  AS  NM_PREVChangeset,
INSERTED.SUPPCD  AS  SUPPCDChangeset,
INSERTED.LIC_AUTHCD  AS  LIC_AUTHCDChangeset,
INSERTED.LIC_AUTH_PREVCD  AS  LIC_AUTH_PREVCDChangeset,
INSERTED.LIC_AUTHCHANGECD  AS  LIC_AUTHCHANGECDChangeset,
INSERTED.LIC_AUTHCHANGEDT  AS  LIC_AUTHCHANGEDTChangeset,
INSERTED.COMBPRODCD  AS  COMBPRODCDChangeset,
INSERTED.FLAVOURCD  AS  FLAVOURCDChangeset,
INSERTED.EMA  AS  EMAChangeset,
INSERTED.PARALLEL_IMPORT  AS  PARALLEL_IMPORTChangeset,
INSERTED.AVAIL_RESTRICTCD  AS  AVAIL_RESTRICTCDChangeset,
INSERTED.AMPS_Id  AS  AMPS_IdChangeset,
@DmdChangeSetDetailID

INTO Dmd_Amp_History;

INSERT INTO Dmd_Amp_Changeset_Backup
SELECT 
APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,AMPS_Id,ChangsetId,ChangesetDate,IsAdded,IsUpdated,IsDeleted FROM Dmd_Amp_Changeset

TRUNCATE TABLE Dmd_Amp_Changeset

----------------------------------------- 06.Dmd_Form --------------------------------------------------------------------
MERGE Dmd_Form AS TARGET
USING Dmd_Form_Changeset AS SOURCE 
ON (TARGET.CD = SOURCE.CD) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 
ISNULL(TARGET.CDDT,'''') <> ISNULL(SOURCE.CDDT,'''') OR
ISNULL(TARGET.CDPREV,'''') <> ISNULL(SOURCE.CDPREV,'''') OR
ISNULL(TARGET.[DESC],'''') <> ISNULL(SOURCE.[DESC],'''')

THEN 
UPDATE SET 
TARGET.CDDT = SOURCE.CDDT, 
TARGET.CDPREV = SOURCE.CDPREV,
TARGET.[DESC] = SOURCE.[DESC]

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (CD,CDDT,CDPREV,[DESC]) 
VALUES (SOURCE.CD,SOURCE.CDDT, SOURCE.CDPREV, SOURCE.[DESC])
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,  
DELETED.CD AS CDLive, 
DELETED.CDDT AS CDDTLive, 
DELETED.CDPREV AS CDDTLive, 
DELETED.[DESC] AS DESCLive, 
INSERTED.CD AS CDChangeset, 
INSERTED.CDDT AS CDDTChangeset, 
INSERTED.CDPREV AS CDPREVChangeset, 
INSERTED.[DESC] AS DESCChangeset, 
@DmdChangeSetDetailID
INTO Dmd_Form_History;

INSERT INTO Dmd_Form_Changeset_Backup (CD,CDDT,CDPREV,[DESC],ChangsetId,ChangesetDate)
SELECT CD,CDDT,CDPREV,[DESC],ChangsetId,ChangesetDate FROM Dmd_Form_Changeset

TRUNCATE TABLE Dmd_Form_Changeset


----------------------------------------- 07.Dmd_ROUTE --------------------------------------------------------------------
MERGE Dmd_ROUTE AS TARGET
USING Dmd_ROUTE_Changeset AS SOURCE 
ON (TARGET.CD = SOURCE.CD) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.[DESC],'''') <> ISNULL(SOURCE.[DESC],'''')

THEN 
UPDATE SET 
TARGET.[DESC] = SOURCE.[DESC]

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (CD,[DESC]) 
VALUES (SOURCE.CD,SOURCE.[DESC])
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,  
DELETED.CD AS CDLive, 
DELETED.[DESC] AS DESCLive, 
INSERTED.CD AS CDChangeset, 
INSERTED.[DESC] AS DESCChangeset, 
@DmdChangeSetDetailID
INTO Dmd_ROUTE_History;

INSERT INTO Dmd_ROUTE_Changeset_Backup (CD,[DESC],ChangsetId,ChangesetDate)
SELECT CD,[DESC],ChangsetId,ChangesetDate FROM Dmd_ROUTE_Changeset

TRUNCATE TABLE Dmd_ROUTE_Changeset

----------------------------------------- 08.Dmd_SUPPLIER --------------------------------------------------------------------
MERGE Dmd_SUPPLIER AS TARGET
USING Dmd_SUPPLIER_Changeset AS SOURCE 
ON (TARGET.CD = SOURCE.CD) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.[DESC],'''') <> ISNULL(SOURCE.[DESC],'''')

THEN 
UPDATE SET 
TARGET.[DESC] = SOURCE.[DESC]

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (CD,[DESC]) 
VALUES (SOURCE.CD,SOURCE.[DESC])
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,  
DELETED.CD AS CDLive, 
DELETED.[DESC] AS DESCLive, 
INSERTED.CD AS CDChangeset, 
INSERTED.[DESC] AS DESCChangeset, 
@DmdChangeSetDetailID
INTO Dmd_SUPPLIER_History;

INSERT INTO Dmd_SUPPLIER_Changeset_Backup (CD,[DESC],ChangsetId,ChangesetDate)
SELECT CD,[DESC],ChangsetId,ChangesetDate FROM Dmd_SUPPLIER_Changeset

TRUNCATE TABLE Dmd_SUPPLIER_Changeset


----------------------------------------- 09.Dmd_Gtin --------------------------------------------------------------------
MERGE Dmd_Gtin AS TARGET
USING Dmd_Gtin_Changeset AS SOURCE 
ON (TARGET.AMPPID = SOURCE.AMPPID) 
--When records are matched, update 
--the records if there is any change
WHEN MATCHED AND 

ISNULL(TARGET.GTINDATAGTIN,'''') <> ISNULL(SOURCE.GTINDATAGTIN,'''') OR
ISNULL(TARGET.GTINDATASTARTDT,'''') <> ISNULL(SOURCE.GTINDATASTARTDT,'''')

THEN 
UPDATE SET 
TARGET.GTINDATAGTIN=SOURCE.GTINDATAGTIN,
TARGET.GTINDATASTARTDT=SOURCE.GTINDATASTARTDT

--When no records are matched, insert
--the incoming records from source
--table to target table
WHEN NOT MATCHED BY TARGET THEN 
INSERT (AMPPID,GTINDATAGTIN,GTINDATASTARTDT) 
VALUES (SOURCE.AMPPID,SOURCE.GTINDATAGTIN,SOURCE.GTINDATASTARTDT)
--When there is a row that exists in target table and
--same record does not exist in source table
--then delete this record from target table
WHEN NOT MATCHED BY SOURCE THEN 
DELETE
--$action specifies a column of type nvarchar(10) 
--in the OUTPUT clause that returns one of three 
--values for each row: ''INSERT'', ''UPDATE'', or ''DELETE'', 
--according to the action that was performed on that row

OUTPUT CASE WHEN $action=''INSERT'' THEN ''ToBeInserted''
WHEN $action=''UPDATE'' THEN ''ToBeUpdated''
WHEN $action=''DELETE'' THEN ''ToBeDeleted'' ELSE '''' END  AS ActionType,
DELETED.AMPPID AS AMPPIDLive, 
DELETED.GTINDATAGTIN AS GTINDATAGTINLive, 
DELETED.GTINDATASTARTDT AS GTINDATASTARTDTLive, 
INSERTED.AMPPID AS AMPPIDChangeset, 
INSERTED.GTINDATAGTIN AS GTINDATAGTINChangeset,  
INSERTED.GTINDATASTARTDT AS GTINDATASTARTDTChangeset, 
@DmdChangeSetDetailID
INTO Dmd_Gtin_History;

INSERT INTO Dmd_Gtin_Changeset_Backup (AMPPID,GTINDATAGTIN,GTINDATASTARTDT,ChangsetId,ChangesetDate)
SELECT AMPPID,GTINDATAGTIN,GTINDATASTARTDT,ChangsetId,ChangesetDate FROM Dmd_Gtin_Changeset

TRUNCATE TABLE Dmd_Gtin_Changeset


END



' 
END
GO
/****** Object:  Table [dbo].[TrustContacts]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrustContacts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrustContacts](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ContactId] [bigint] NOT NULL,
	[TrustId] [int] NOT NULL,
 CONSTRAINT [PK_TrustContacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [task].[ReminderInvitation]    Script Date: 07/14/2018 21:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderInvitation]') AND type in (N'U'))
BEGIN
CREATE TABLE [task].[ReminderInvitation](
	[ReminderInvitationId] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[ReminderId] [int] NOT NULL,
	[SenderUserName] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReminderInvitation] PRIMARY KEY CLUSTERED 
(
	[ReminderInvitationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [task].[ReminderProgress]    Script Date: 07/14/2018 21:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderProgress]') AND type in (N'U'))
BEGIN
CREATE TABLE [task].[ReminderProgress](
	[ReminderProgressId] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[IsArchived] [bit] NOT NULL,
	[LastProgressDate] [datetime2](7) NULL,
	[ReminderProgressPercent] [int] NOT NULL,
	[ReminderProgressStatusId] [int] NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReminderProgress] PRIMARY KEY CLUSTERED 
(
	[ReminderProgressId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetDmdAmpHistory]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetDmdAmpHistory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetDmdAmpHistory]
(
  /* Optional Filters for Dynamic Search*/
  @APIDLive bigint=NULL,
  @INVALIDLive decimal(28, 0)=NULL,
  @VPIDLive decimal(28, 0)=NULL,
  @NMLive nvarchar(255)=NULL,
  @ABBREVNMLive nvarchar(255)=NULL,
  @DESCLive nvarchar(255)=NULL,
  @NMDTLive datetime=NULL,
  @NM_PREVLive nvarchar(255)=NULL,
  @SUPPCDLive decimal(28, 0)=NULL,
  @LIC_AUTHCDLive decimal(28, 0)=NULL,
  @LIC_AUTH_PREVCDLive decimal(28, 0)=NULL,
  @LIC_AUTHCHANGECDLive decimal(28, 0)=NULL,
  @LIC_AUTHCHANGEDTLive datetime=NULL,
  @COMBPRODCDLive decimal(28, 0)=NULL,
  @FLAVOURCDLive decimal(28, 0)=NULL,
  @EMALive decimal(28, 0)=NULL,
  @PARALLEL_IMPORTLive decimal(28, 0)=NULL,
  @AVAIL_RESTRICTCDLive decimal(28, 0)=NULL,
  @AMPS_IdLive numeric(20, 0)=NULL,
  @APIDChangeset bigint=NULL,
  @INVALIDChangeset decimal(28, 0)=NULL,
  @VPIDChangeset decimal(28, 0)=NULL,
  @NMChangeset nvarchar(255)=NULL,
  @ABBREVNMChangeset nvarchar(255)=NULL,
  @DESCChangeset nvarchar(255)=NULL,
  @NMDTChangeset datetime=NULL,
  @NM_PREVChangeset nvarchar(255)=NULL,
  @SUPPCDChangeset decimal(28, 0)=NULL,
  @LIC_AUTHCDChangeset decimal(28, 0)=NULL,
  @LIC_AUTH_PREVCDChangeset decimal(28, 0)=NULL,
  @LIC_AUTHCHANGECDChangeset decimal(28, 0)=NULL,
  @LIC_AUTHCHANGEDTChangeset datetime=NULL,
  @COMBPRODCDChangeset decimal(28, 0)=NULL,
  @FLAVOURCDChangeset decimal(28, 0)=NULL,
  @EMAChangeset decimal(28, 0)=NULL,
  @PARALLEL_IMPORTChangeset decimal(28, 0)=NULL,
  @AVAIL_RESTRICTCDChangeset decimal(28, 0)=NULL,
  @AMPS_IdChangeset numeric(20, 0)=NULL,
  @DmdChangeSetDetailID int=NULL,
  /*– Pagination Parameters */
  @PageNo INT = 1,
  @PageSize INT = 10,
  /*– Sorting Parameters */
  @SortColumn NVARCHAR(20) = ''Title'',
  @SortOrder NVARCHAR(4)=''ASC''
)
AS
BEGIN
    /*–Declaring Local Variables corresponding to parameters for modification */
    DECLARE 
    @lContactID INT,
    @lFirstName NVARCHAR(50),
    @lLastName NVARCHAR(50),
    @lEmailAddress NVARCHAR(50),
    @lEmailPromotion INT,
    @lPhone NVARCHAR(25),

    @lPageNbr INT,
    @lPageSize INT,
    @lSortCol NVARCHAR(20),
    @lFirstRec INT,
    @lLastRec INT,
    @lTotalRows INT

  

    SET @lPageNbr = @PageNo
    SET @lPageSize = @PageSize
    SET @lSortCol = LTRIM(RTRIM(@SortColumn))

    SET @lFirstRec = ( @lPageNbr - 1 ) * @lPageSize
    SET @lLastRec = ( @lPageNbr * @lPageSize + 1 )
    SET @lTotalRows = @lFirstRec - @lLastRec + 1

    ; WITH CTE_Results
    AS (
    SELECT ROW_NUMBER() OVER (ORDER BY
        CASE WHEN (@lSortCol =''APIDLive'' AND @SortOrder=''ASC'') THEN APIDLive END ASC,CASE WHEN (@lSortCol =  ''APIDLive'' AND @SortOrder=''DESC'')   THEN APIDLive END DESC,
CASE WHEN (@lSortCol =''INVALIDLive'' AND @SortOrder=''ASC'') THEN INVALIDLive END ASC,CASE WHEN (@lSortCol =  ''INVALIDLive'' AND @SortOrder=''DESC'')   THEN INVALIDLive END DESC,
CASE WHEN (@lSortCol =''VPIDLive'' AND @SortOrder=''ASC'') THEN VPIDLive END ASC,CASE WHEN (@lSortCol =  ''VPIDLive'' AND @SortOrder=''DESC'')   THEN VPIDLive END DESC,
CASE WHEN (@lSortCol =''NMLive'' AND @SortOrder=''ASC'') THEN NMLive END ASC,CASE WHEN (@lSortCol =  ''NMLive'' AND @SortOrder=''DESC'')   THEN NMLive END DESC,
CASE WHEN (@lSortCol =''ABBREVNMLive'' AND @SortOrder=''ASC'') THEN ABBREVNMLive END ASC,CASE WHEN (@lSortCol =  ''ABBREVNMLive'' AND @SortOrder=''DESC'')   THEN ABBREVNMLive END DESC,
CASE WHEN (@lSortCol =''DESCLive'' AND @SortOrder=''ASC'') THEN DESCLive END ASC,CASE WHEN (@lSortCol =  ''DESCLive'' AND @SortOrder=''DESC'')   THEN DESCLive END DESC,
CASE WHEN (@lSortCol =''NMDTLive'' AND @SortOrder=''ASC'') THEN NMDTLive END ASC,CASE WHEN (@lSortCol =  ''NMDTLive'' AND @SortOrder=''DESC'')   THEN NMDTLive END DESC,
CASE WHEN (@lSortCol =''NM_PREVLive'' AND @SortOrder=''ASC'') THEN NM_PREVLive END ASC,CASE WHEN (@lSortCol =  ''NM_PREVLive'' AND @SortOrder=''DESC'')   THEN NM_PREVLive END DESC,
CASE WHEN (@lSortCol =''SUPPCDLive'' AND @SortOrder=''ASC'') THEN SUPPCDLive END ASC,CASE WHEN (@lSortCol =  ''SUPPCDLive'' AND @SortOrder=''DESC'')   THEN SUPPCDLive END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCDLive'' AND @SortOrder=''ASC'') THEN LIC_AUTHCDLive END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCDLive'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCDLive END DESC,
CASE WHEN (@lSortCol =''LIC_AUTH_PREVCDLive'' AND @SortOrder=''ASC'') THEN LIC_AUTH_PREVCDLive END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTH_PREVCDLive'' AND @SortOrder=''DESC'')   THEN LIC_AUTH_PREVCDLive END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCHANGECDLive'' AND @SortOrder=''ASC'') THEN LIC_AUTHCHANGECDLive END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCHANGECDLive'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCHANGECDLive END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCHANGEDTLive'' AND @SortOrder=''ASC'') THEN LIC_AUTHCHANGEDTLive END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCHANGEDTLive'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCHANGEDTLive END DESC,
CASE WHEN (@lSortCol =''COMBPRODCDLive'' AND @SortOrder=''ASC'') THEN COMBPRODCDLive END ASC,CASE WHEN (@lSortCol =  ''COMBPRODCDLive'' AND @SortOrder=''DESC'')   THEN COMBPRODCDLive END DESC,
CASE WHEN (@lSortCol =''FLAVOURCDLive'' AND @SortOrder=''ASC'') THEN FLAVOURCDLive END ASC,CASE WHEN (@lSortCol =  ''FLAVOURCDLive'' AND @SortOrder=''DESC'')   THEN FLAVOURCDLive END DESC,
CASE WHEN (@lSortCol =''EMALive'' AND @SortOrder=''ASC'') THEN EMALive END ASC,CASE WHEN (@lSortCol =  ''EMALive'' AND @SortOrder=''DESC'')   THEN EMALive END DESC,
CASE WHEN (@lSortCol =''PARALLEL_IMPORTLive'' AND @SortOrder=''ASC'') THEN PARALLEL_IMPORTLive END ASC,CASE WHEN (@lSortCol =  ''PARALLEL_IMPORTLive'' AND @SortOrder=''DESC'')   THEN PARALLEL_IMPORTLive END DESC,
CASE WHEN (@lSortCol =''AVAIL_RESTRICTCDLive'' AND @SortOrder=''ASC'') THEN AVAIL_RESTRICTCDLive END ASC,CASE WHEN (@lSortCol =  ''AVAIL_RESTRICTCDLive'' AND @SortOrder=''DESC'')   THEN AVAIL_RESTRICTCDLive END DESC,
CASE WHEN (@lSortCol =''AMPS_IdLive'' AND @SortOrder=''ASC'') THEN AMPS_IdLive END ASC,CASE WHEN (@lSortCol =  ''AMPS_IdLive'' AND @SortOrder=''DESC'')   THEN AMPS_IdLive END DESC,
CASE WHEN (@lSortCol =''APIDChangeset'' AND @SortOrder=''ASC'') THEN APIDChangeset END ASC,CASE WHEN (@lSortCol =  ''APIDChangeset'' AND @SortOrder=''DESC'')   THEN APIDChangeset END DESC,
CASE WHEN (@lSortCol =''INVALIDChangeset'' AND @SortOrder=''ASC'') THEN INVALIDChangeset END ASC,CASE WHEN (@lSortCol =  ''INVALIDChangeset'' AND @SortOrder=''DESC'')   THEN INVALIDChangeset END DESC,
CASE WHEN (@lSortCol =''VPIDChangeset'' AND @SortOrder=''ASC'') THEN VPIDChangeset END ASC,CASE WHEN (@lSortCol =  ''VPIDChangeset'' AND @SortOrder=''DESC'')   THEN VPIDChangeset END DESC,
CASE WHEN (@lSortCol =''NMChangeset'' AND @SortOrder=''ASC'') THEN NMChangeset END ASC,CASE WHEN (@lSortCol =  ''NMChangeset'' AND @SortOrder=''DESC'')   THEN NMChangeset END DESC,
CASE WHEN (@lSortCol =''ABBREVNMChangeset'' AND @SortOrder=''ASC'') THEN ABBREVNMChangeset END ASC,CASE WHEN (@lSortCol =  ''ABBREVNMChangeset'' AND @SortOrder=''DESC'')   THEN ABBREVNMChangeset END DESC,
CASE WHEN (@lSortCol =''DESCChangeset'' AND @SortOrder=''ASC'') THEN DESCChangeset END ASC,CASE WHEN (@lSortCol =  ''DESCChangeset'' AND @SortOrder=''DESC'')   THEN DESCChangeset END DESC,
CASE WHEN (@lSortCol =''NMDTChangeset'' AND @SortOrder=''ASC'') THEN NMDTChangeset END ASC,CASE WHEN (@lSortCol =  ''NMDTChangeset'' AND @SortOrder=''DESC'')   THEN NMDTChangeset END DESC,
CASE WHEN (@lSortCol =''NM_PREVChangeset'' AND @SortOrder=''ASC'') THEN NM_PREVChangeset END ASC,CASE WHEN (@lSortCol =  ''NM_PREVChangeset'' AND @SortOrder=''DESC'')   THEN NM_PREVChangeset END DESC,
CASE WHEN (@lSortCol =''SUPPCDChangeset'' AND @SortOrder=''ASC'') THEN SUPPCDChangeset END ASC,CASE WHEN (@lSortCol =  ''SUPPCDChangeset'' AND @SortOrder=''DESC'')   THEN SUPPCDChangeset END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCDChangeset'' AND @SortOrder=''ASC'') THEN LIC_AUTHCDChangeset END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCDChangeset'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCDChangeset END DESC,
CASE WHEN (@lSortCol =''LIC_AUTH_PREVCDChangeset'' AND @SortOrder=''ASC'') THEN LIC_AUTH_PREVCDChangeset END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTH_PREVCDChangeset'' AND @SortOrder=''DESC'')   THEN LIC_AUTH_PREVCDChangeset END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCHANGECDChangeset'' AND @SortOrder=''ASC'') THEN LIC_AUTHCHANGECDChangeset END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCHANGECDChangeset'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCHANGECDChangeset END DESC,
CASE WHEN (@lSortCol =''LIC_AUTHCHANGEDTChangeset'' AND @SortOrder=''ASC'') THEN LIC_AUTHCHANGEDTChangeset END ASC,CASE WHEN (@lSortCol =  ''LIC_AUTHCHANGEDTChangeset'' AND @SortOrder=''DESC'')   THEN LIC_AUTHCHANGEDTChangeset END DESC,
CASE WHEN (@lSortCol =''COMBPRODCDChangeset'' AND @SortOrder=''ASC'') THEN COMBPRODCDChangeset END ASC,CASE WHEN (@lSortCol =  ''COMBPRODCDChangeset'' AND @SortOrder=''DESC'')   THEN COMBPRODCDChangeset END DESC,
CASE WHEN (@lSortCol =''FLAVOURCDChangeset'' AND @SortOrder=''ASC'') THEN FLAVOURCDChangeset END ASC,CASE WHEN (@lSortCol =  ''FLAVOURCDChangeset'' AND @SortOrder=''DESC'')   THEN FLAVOURCDChangeset END DESC,
CASE WHEN (@lSortCol =''EMAChangeset'' AND @SortOrder=''ASC'') THEN EMAChangeset END ASC,CASE WHEN (@lSortCol =  ''EMAChangeset'' AND @SortOrder=''DESC'')   THEN EMAChangeset END DESC,
CASE WHEN (@lSortCol =''PARALLEL_IMPORTChangeset'' AND @SortOrder=''ASC'') THEN PARALLEL_IMPORTChangeset END ASC,CASE WHEN (@lSortCol =  ''PARALLEL_IMPORTChangeset'' AND @SortOrder=''DESC'')   THEN PARALLEL_IMPORTChangeset END DESC,
CASE WHEN (@lSortCol =''AVAIL_RESTRICTCDChangeset'' AND @SortOrder=''ASC'') THEN AVAIL_RESTRICTCDChangeset END ASC,CASE WHEN (@lSortCol =  ''AVAIL_RESTRICTCDChangeset'' AND @SortOrder=''DESC'')   THEN AVAIL_RESTRICTCDChangeset END DESC,
CASE WHEN (@lSortCol =''AMPS_IdChangeset'' AND @SortOrder=''ASC'') THEN AMPS_IdChangeset END ASC,CASE WHEN (@lSortCol =  ''AMPS_IdChangeset'' AND @SortOrder=''DESC'')   THEN AMPS_IdChangeset END DESC,
CASE WHEN (@lSortCol =''DmdChangeSetDetailID'' AND @SortOrder=''ASC'') THEN DmdChangeSetDetailID END ASC,CASE WHEN (@lSortCol =  ''DmdChangeSetDetailID'' AND @SortOrder=''DESC'')   THEN DmdChangeSetDetailID END DESC


  ) AS ROWNUM,
     Count(*) over () AS TotalCount,
     APIDLive,
     INVALIDLive,
     VPIDLive,
     NMLive,
     ABBREVNMLive,
     DESCLive,
     NMDTLive,
     NM_PREVLive,
     SUPPCDLive,
     LIC_AUTHCDLive,
     LIC_AUTH_PREVCDLive,
     LIC_AUTHCHANGECDLive,
     LIC_AUTHCHANGEDTLive,
     COMBPRODCDLive,
     FLAVOURCDLive,
     EMALive,
     PARALLEL_IMPORTLive,
     AVAIL_RESTRICTCDLive,
     AMPS_IdLive,
     APIDChangeset,
     INVALIDChangeset,
     VPIDChangeset,
     NMChangeset,
     ABBREVNMChangeset,
     DESCChangeset,
     NMDTChangeset,
     NM_PREVChangeset,
     SUPPCDChangeset,
     LIC_AUTHCDChangeset,
     LIC_AUTH_PREVCDChangeset,
     LIC_AUTHCHANGECDChangeset,
     LIC_AUTHCHANGEDTChangeset,
     COMBPRODCDChangeset,
     FLAVOURCDChangeset,
     EMAChangeset,
     PARALLEL_IMPORTChangeset,
     AVAIL_RESTRICTCDChangeset,
     AMPS_IdChangeset,
     DmdChangeSetDetailID


 FROM Dmd_Amp_History
 WHERE
      (@APIDLive IS NULL OR APIDLive  LIKE ''%'' +@APIDLive+ ''%'')
AND(@INVALIDLive IS NULL OR INVALIDLive  LIKE ''%'' +@INVALIDLive+ ''%'')
AND(@VPIDLive IS NULL OR VPIDLive  LIKE ''%'' +@VPIDLive+ ''%'')
AND(@NMLive IS NULL OR NMLive  LIKE ''%'' +@NMLive+ ''%'')
AND(@ABBREVNMLive IS NULL OR ABBREVNMLive  LIKE ''%'' +@ABBREVNMLive+ ''%'')
AND(@DESCLive IS NULL OR DESCLive  LIKE ''%'' +@DESCLive+ ''%'')
AND(@NMDTLive IS NULL OR NMDTLive  LIKE ''%'' +@NMDTLive+ ''%'')
AND(@NM_PREVLive IS NULL OR NM_PREVLive  LIKE ''%'' +@NM_PREVLive+ ''%'')
AND(@SUPPCDLive IS NULL OR SUPPCDLive  LIKE ''%'' +@SUPPCDLive+ ''%'')
AND(@LIC_AUTHCDLive IS NULL OR LIC_AUTHCDLive  LIKE ''%'' +@LIC_AUTHCDLive+ ''%'')
AND(@LIC_AUTH_PREVCDLive IS NULL OR LIC_AUTH_PREVCDLive  LIKE ''%'' +@LIC_AUTH_PREVCDLive+ ''%'')
AND(@LIC_AUTHCHANGECDLive IS NULL OR LIC_AUTHCHANGECDLive  LIKE ''%'' +@LIC_AUTHCHANGECDLive+ ''%'')
AND(@LIC_AUTHCHANGEDTLive IS NULL OR LIC_AUTHCHANGEDTLive  LIKE ''%'' +@LIC_AUTHCHANGEDTLive+ ''%'')
AND(@COMBPRODCDLive IS NULL OR COMBPRODCDLive  LIKE ''%'' +@COMBPRODCDLive+ ''%'')
AND(@FLAVOURCDLive IS NULL OR FLAVOURCDLive  LIKE ''%'' +@FLAVOURCDLive+ ''%'')
AND(@EMALive IS NULL OR EMALive  LIKE ''%'' +@EMALive+ ''%'')
AND(@PARALLEL_IMPORTLive IS NULL OR PARALLEL_IMPORTLive  LIKE ''%'' +@PARALLEL_IMPORTLive+ ''%'')
AND(@AVAIL_RESTRICTCDLive IS NULL OR AVAIL_RESTRICTCDLive  LIKE ''%'' +@AVAIL_RESTRICTCDLive+ ''%'')
AND(@AMPS_IdLive IS NULL OR AMPS_IdLive  LIKE ''%'' +@AMPS_IdLive+ ''%'')
AND(@APIDChangeset IS NULL OR APIDChangeset  LIKE ''%'' +@APIDChangeset+ ''%'')
AND(@INVALIDChangeset IS NULL OR INVALIDChangeset  LIKE ''%'' +@INVALIDChangeset+ ''%'')
AND(@VPIDChangeset IS NULL OR VPIDChangeset  LIKE ''%'' +@VPIDChangeset+ ''%'')
AND(@NMChangeset IS NULL OR NMChangeset  LIKE ''%'' +@NMChangeset+ ''%'')
AND(@ABBREVNMChangeset IS NULL OR ABBREVNMChangeset  LIKE ''%'' +@ABBREVNMChangeset+ ''%'')
AND(@DESCChangeset IS NULL OR DESCChangeset  LIKE ''%'' +@DESCChangeset+ ''%'')
AND(@NMDTChangeset IS NULL OR NMDTChangeset  LIKE ''%'' +@NMDTChangeset+ ''%'')
AND(@NM_PREVChangeset IS NULL OR NM_PREVChangeset  LIKE ''%'' +@NM_PREVChangeset+ ''%'')
AND(@SUPPCDChangeset IS NULL OR SUPPCDChangeset  LIKE ''%'' +@SUPPCDChangeset+ ''%'')
AND(@LIC_AUTHCDChangeset IS NULL OR LIC_AUTHCDChangeset  LIKE ''%'' +@LIC_AUTHCDChangeset+ ''%'')
AND(@LIC_AUTH_PREVCDChangeset IS NULL OR LIC_AUTH_PREVCDChangeset  LIKE ''%'' +@LIC_AUTH_PREVCDChangeset+ ''%'')
AND(@LIC_AUTHCHANGECDChangeset IS NULL OR LIC_AUTHCHANGECDChangeset  LIKE ''%'' +@LIC_AUTHCHANGECDChangeset+ ''%'')
AND(@LIC_AUTHCHANGEDTChangeset IS NULL OR LIC_AUTHCHANGEDTChangeset  LIKE ''%'' +@LIC_AUTHCHANGEDTChangeset+ ''%'')
AND(@COMBPRODCDChangeset IS NULL OR COMBPRODCDChangeset  LIKE ''%'' +@COMBPRODCDChangeset+ ''%'')
AND(@FLAVOURCDChangeset IS NULL OR FLAVOURCDChangeset  LIKE ''%'' +@FLAVOURCDChangeset+ ''%'')
AND(@EMAChangeset IS NULL OR EMAChangeset  LIKE ''%'' +@EMAChangeset+ ''%'')
AND(@PARALLEL_IMPORTChangeset IS NULL OR PARALLEL_IMPORTChangeset  LIKE ''%'' +@PARALLEL_IMPORTChangeset+ ''%'')
AND(@AVAIL_RESTRICTCDChangeset IS NULL OR AVAIL_RESTRICTCDChangeset  LIKE ''%'' +@AVAIL_RESTRICTCDChangeset+ ''%'')
AND(@AMPS_IdChangeset IS NULL OR AMPS_IdChangeset  LIKE ''%'' +@AMPS_IdChangeset+ ''%'')
AND(@DmdChangeSetDetailID IS NULL OR DmdChangeSetDetailID  LIKE ''%'' +@DmdChangeSetDetailID+ ''%'')


)
SELECT
    TotalCount,
    ROWNUM,
    APIDLive,
    INVALIDLive,
    VPIDLive,
    NMLive,
    ABBREVNMLive,
    DESCLive,
    NMDTLive,
    NM_PREVLive,
    SUPPCDLive,
    LIC_AUTHCDLive,
    LIC_AUTH_PREVCDLive,
    LIC_AUTHCHANGECDLive,
    LIC_AUTHCHANGEDTLive,
    COMBPRODCDLive,
    FLAVOURCDLive,
    EMALive,
    PARALLEL_IMPORTLive,
    AVAIL_RESTRICTCDLive,
    AMPS_IdLive,
    APIDChangeset,
    INVALIDChangeset,
    VPIDChangeset,
    NMChangeset,
    ABBREVNMChangeset,
    DESCChangeset,
    NMDTChangeset,
    NM_PREVChangeset,
    SUPPCDChangeset,
    LIC_AUTHCDChangeset,
    LIC_AUTH_PREVCDChangeset,
    LIC_AUTHCHANGECDChangeset,
    LIC_AUTHCHANGEDTChangeset,
    COMBPRODCDChangeset,
    FLAVOURCDChangeset,
    EMAChangeset,
    PARALLEL_IMPORTChangeset,
    AVAIL_RESTRICTCDChangeset,
    AMPS_IdChangeset,
    DmdChangeSetDetailID
    
FROM CTE_Results AS CPC
WHERE
        ROWNUM > @lFirstRec
              AND ROWNUM < @lLastRec
 ORDER BY ROWNUM ASC

END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLToStaging_01]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLToStaging_01]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

create PROCEDURE [dbo].[SP_DmdProcessXMLToStaging_01]
       -- Add the parameters for the stored procedure here
       @xmlData XML,
	   @TableName varchar(100)
AS
BEGIN
      
	  DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)

       SET NOCOUNT ON;

    -- Insert statements for f_vtm2

     IF(@TableName=''f_vtm2'')

	 BEGIN	

           EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
          
           INSERT INTO Dmd_Vtm_Changeset (VTMID,INVALID,NM,ABBREVNM,VTMIDPREV,VTMIDDT)
           SELECT VTMID, INVALID, NM,ABBREVNM,VTMIDPREV,VTMIDDT
           FROM OPENXML(@hDoc, ''/VIRTUAL_THERAPEUTIC_MOIETIES/VTM'')
           WITH 
           (
           VTMID bigint ''VTMID'',
           INVALID [decimal](28, 0) ''INVALID'',
           NM nvarchar(2550) ''NM'',
           ABBREVNM nvarchar(2550) ''ABBREVNM'',
           VTMIDPREV nvarchar(2550) ''VTMIDPREV'',
           VTMIDDT nvarchar(2550) ''VTMIDDT''
           )
           EXEC sp_xml_removedocument @hDoc
           

	   END

	    -- Insert statements for f_vmpp2
    IF(@TableName=''f_vmpp2'')

	BEGIN
		EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
		
        INSERT INTO Dmd_Vmpp_Changeset (VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id)
        SELECT VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id
        FROM OPENXML(@hDoc, ''/VIRTUAL_MED_PRODUCT_PACK/VMPPS/VMPP'')
        WITH 
        (
        VPPID bigint ''VPPID'',
        INVALID [decimal](28, 0) ''INVALID'',
        NM nvarchar(2550) ''NM'',
        ABBREVNM nvarchar(2550) ''ABBREVNM'',
        VPID decimal(28, 0) ''VPID'',
        QTYVAL real ''QTYVAL'',
        QTY_UOMCD decimal(28, 0) ''QTY_UOMCD'',
        COMBPACKCD decimal(28, 0) ''COMBPACKCD'',
        VMPPS_Id numeric (20, 0) ''VMPPS_Id''
        
        )
		EXEC sp_xml_removedocument @hDoc

	END

	   
	    -- Insert statements for f_vmp2
   IF(@TableName=''f_vmp2'')

	 BEGIN
	     EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData

          INSERT INTO Dmd_Vmp_Changeset (VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,
          UDFS,UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id)
          
          SELECT VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,
          UDFS,UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id
          
          FROM OPENXML(@hDoc, ''/VIRTUAL_MED_PRODUCTS/VMPS/VMP'')
          WITH 
          (
          VPID bigint ''VPID'',
          VPIDDT  datetime ''VPIDDT'',
          VPIDPREV  decimal(28, 0) ''VPIDPREV'',
          VTMID  decimal(28, 0) ''VTMID'' ,
          INVALID  decimal(28, 0) ''INVALID'',
          NM  nvarchar(255) ''NM'',
          ABBREVNM  nvarchar(255) ''ABBREVNM'' ,
          BASISCD  decimal(28, 0) ''BASISCD'',
          NMDT  datetime ''NMDT'',
          NMPREV  nvarchar(255) ''NMPREV'' ,
          BASIS_PREVCD  decimal(28, 0) ''BASIS_PREVCD'' ,
          NMCHANGECD  decimal(28, 0) ''NMCHANGECD'',
          COMBPRODCD  decimal(28, 0) ''COMBPRODCD'',
          PRES_STATCD  decimal(28, 0) ''PRES_STATCD'',
          SUG_F  decimal(28, 0) ''SUG_F'',
          GLU_F  decimal(28, 0) ''GLU_F'',
          PRES_F  decimal(28, 0) ''PRES_F'',
          CFC_F  decimal(28, 0) ''CFC_F'',
          NON_AVAILCD  decimal(28, 0) ''NON_AVAILCD'',
          NON_AVAILDT  datetime ''NON_AVAILDT'',
          DF_INDCD  decimal(28, 0) ''DF_INDCD'',
          UDFS  real ''UDFS'',
          UDFS_UOMCD  decimal(28, 0) ''UDFS_UOMCD'',
          UNIT_DOSE_UOMCD  decimal(28, 0) ''UNIT_DOSE_UOMCD'',
          VMPS_Id  numeric(20, 0) ''VMPS_Id''
          
          )
          EXEC sp_xml_removedocument @hDoc

	 END

	    -- Insert statements for f_amp2
	IF(@TableName=''f_amp2'')

	 BEGIN
	
        EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
        
        INSERT INTO Dmd_Amp_Changeset (APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,
        AMPS_Id)
        
        SELECT APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,
        AMPS_Id
        
        FROM OPENXML(@hDoc, ''/ACTUAL_MEDICINAL_PRODUCTS/AMPS/AMP'')
        WITH 
        (
         APID  bigint  ''APID'',
         INVALID  decimal (28, 0) ''INVALID'',
         VPID  decimal (28, 0) ''VPID'',
         NM  nvarchar (255) ''NM'',
         ABBREVNM  nvarchar (255) ''ABBREVNM'',
         [DESC]  nvarchar (255) ''DESC'',
         NMDT  datetime ''NMDT'',
         NM_PREV  nvarchar (255) ''NM_PREV'',
         SUPPCD  decimal (28, 0) ''SUPPCD'',
         LIC_AUTHCD  decimal (28, 0) ''LIC_AUTHCD'',
         LIC_AUTH_PREVCD  decimal (28, 0) ''LIC_AUTH_PREVCD'',
         LIC_AUTHCHANGECD  decimal (28, 0) ''LIC_AUTHCHANGECD'',
         LIC_AUTHCHANGEDT  datetime ''LIC_AUTHCHANGEDT'',
         COMBPRODCD  decimal (28, 0) ''COMBPRODCD'',
         FLAVOURCD  decimal (28, 0) ''FLAVOURCD'',
         EMA  decimal (28, 0) ''EMA'',
         PARALLEL_IMPORT  decimal (28, 0) ''PARALLEL_IMPORT'',
         AVAIL_RESTRICTCD  decimal (28, 0) ''AVAIL_RESTRICTCD'',
         AMPS_Id  numeric (20, 0) ''AMPS_Id''
        
        
        )
        EXEC sp_xml_removedocument @hDoc
        
	 
	 END



	     -- Insert statements for f_ampp2
	 IF(@TableName=''f_ampp2'')

	 BEGIN
	 PRINT ''''
         EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
         
         INSERT INTO Dmd_Ampp_Changeset (APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id)
         
         SELECT APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id
         
         FROM OPENXML(@hDoc, ''/ACTUAL_MEDICINAL_PROD_PACKS/AMPPS/AMPP'')
         WITH 
         (
         APPID bigint  ''APPID'',
         INVALID decimal (28, 0) ''INVALID'',
         NM nvarchar (255) ''NM'',
         ABBREVNM nvarchar (255) ''ABBREVNM'',
         VPPID decimal (28, 0) ''VPPID'',
         APID decimal (28, 0) ''APID'',
         COMBPACKCD decimal (28, 0) ''COMBPACKCD'',
         LEGAL_CATCD decimal (28, 0) ''LEGAL_CATCD'',
         SUBP nvarchar (255) ''SUBP'',
         DISCCD decimal (28, 0) ''DISCCD'',
         DISCDT datetime ''DISCDT'',
         AMPPS_Id numeric (20, 0) ''AMPPS_Id''
         
         
         )
         EXEC sp_xml_removedocument @hDoc

	   END
END



' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLToStaging]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLToStaging]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'





CREATE PROCEDURE [dbo].[SP_DmdProcessXMLToStaging]
       -- Add the parameters for the stored procedure here
       @xmlData XML,
	   @TableName VARCHAR(100),
	   @DmdChangeSetDetailID INT
AS
BEGIN
      
	  DECLARE @XML AS XML, @hDoc AS INT, @SQL NVARCHAR (MAX)

       SET NOCOUNT ON;

    -- Insert statements for f_vtm2

     IF(@TableName=''f_vtm2'')

	 BEGIN	

           EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
          
           INSERT INTO Dmd_Vtm_Changeset (VTMID,INVALID,NM,ABBREVNM,VTMIDPREV,VTMIDDT)
           SELECT VTMID, INVALID, NM,ABBREVNM,VTMIDPREV,VTMIDDT
           FROM OPENXML(@hDoc, ''/VIRTUAL_THERAPEUTIC_MOIETIES/VTM'')
           WITH 
           (
           VTMID nvarchar(max) ''VTMID'',
           INVALID nvarchar(max) ''INVALID'',
           NM nvarchar(max) ''NM'',
           ABBREVNM nvarchar(max) ''ABBREVNM'',
           VTMIDPREV nvarchar(max) ''VTMIDPREV'',
           VTMIDDT nvarchar(max) ''VTMIDDT''
           )
           EXEC sp_xml_removedocument @hDoc
           
		   UPDATE Dmd_Vtm_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
	   END

	    -- Insert statements for f_vmpp2
    IF(@TableName=''f_vmpp2'')

	BEGIN
		EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
		
        INSERT INTO Dmd_Vmpp_Changeset (VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id)
        SELECT VPPID,INVALID,NM,ABBREVNM,VPID,QTYVAL,QTY_UOMCD,COMBPACKCD,VMPPS_Id
        FROM OPENXML(@hDoc, ''/VIRTUAL_MED_PRODUCT_PACK/VMPPS/VMPP'')
        WITH 
        (
        VPPID nvarchar(max) ''VPPID'',
        INVALID nvarchar(max) ''INVALID'',
        NM nvarchar(max) ''NM'',
        ABBREVNM nvarchar(max) ''ABBREVNM'',
        VPID nvarchar(max) ''VPID'',
        QTYVAL nvarchar(max) ''QTYVAL'',
        QTY_UOMCD nvarchar(max) ''QTY_UOMCD'',
        COMBPACKCD nvarchar(max) ''COMBPACKCD'',
        VMPPS_Id nvarchar(max) ''VMPPS_Id''
        
        )
		EXEC sp_xml_removedocument @hDoc

		UPDATE Dmd_Vmpp_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL

	END

	   
	    -- Insert statements for f_vmp2
   IF(@TableName=''f_vmp2'')

	 BEGIN
	     EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData

          INSERT INTO Dmd_Vmp_Changeset (VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,
          UDFS,UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id)
          
          SELECT VPID,VPIDDT,VPIDPREV,VTMID,INVALID,NM,ABBREVNM,BASISCD,NMDT,NMPREV,BASIS_PREVCD,NMCHANGECD,COMBPRODCD,PRES_STATCD,SUG_F,GLU_F,PRES_F,CFC_F,NON_AVAILCD,NON_AVAILDT,DF_INDCD,
          UDFS,UDFS_UOMCD,UNIT_DOSE_UOMCD,VMPS_Id
          
          FROM OPENXML(@hDoc, ''/VIRTUAL_MED_PRODUCTS/VMPS/VMP'')
          WITH 
          (
          VPID nvarchar(max) ''VPID'',
          VPIDDT  nvarchar(max) ''VPIDDT'',
          VPIDPREV  nvarchar(max) ''VPIDPREV'',
          VTMID  nvarchar(max) ''VTMID'' ,
          INVALID  nvarchar(max) ''INVALID'',
          NM  nvarchar(max) ''NM'',
          ABBREVNM  nvarchar(max) ''ABBREVNM'' ,
          BASISCD  nvarchar(max) ''BASISCD'',
          NMDT  nvarchar(max) ''NMDT'',
          NMPREV  nvarchar(max) ''NMPREV'' ,
          BASIS_PREVCD  nvarchar(max) ''BASIS_PREVCD'' ,
          NMCHANGECD  nvarchar(max) ''NMCHANGECD'',
          COMBPRODCD  nvarchar(max) ''COMBPRODCD'',
          PRES_STATCD  nvarchar(max) ''PRES_STATCD'',
          SUG_F  nvarchar(max) ''SUG_F'',
          GLU_F  nvarchar(max) ''GLU_F'',
          PRES_F  nvarchar(max) ''PRES_F'',
          CFC_F  nvarchar(max) ''CFC_F'',
          NON_AVAILCD  nvarchar(max) ''NON_AVAILCD'',
          NON_AVAILDT  nvarchar(max) ''NON_AVAILDT'',
          DF_INDCD  nvarchar(max) ''DF_INDCD'',
          UDFS  nvarchar(max) ''UDFS'',
          UDFS_UOMCD  nvarchar(max) ''UDFS_UOMCD'',
          UNIT_DOSE_UOMCD  nvarchar(max) ''UNIT_DOSE_UOMCD'',
          VMPS_Id  nvarchar(max) ''VMPS_Id''
          
          )
          EXEC sp_xml_removedocument @hDoc

		  UPDATE Dmd_Vmp_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
	 END

	    -- Insert statements for f_amp2
	IF(@TableName=''f_amp2'')

	 BEGIN
	
        EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
        
        INSERT INTO Dmd_Amp_Changeset (APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,
        AMPS_Id)
        
        SELECT APID,INVALID,VPID,NM,ABBREVNM,[DESC],NMDT,NM_PREV,SUPPCD,LIC_AUTHCD,LIC_AUTH_PREVCD,LIC_AUTHCHANGECD,LIC_AUTHCHANGEDT,COMBPRODCD,FLAVOURCD,EMA,PARALLEL_IMPORT,AVAIL_RESTRICTCD,
        AMPS_Id
        
        FROM OPENXML(@hDoc, ''/ACTUAL_MEDICINAL_PRODUCTS/AMPS/AMP'')
        WITH 
        (
         APID  nvarchar(max)  ''APID'',
         INVALID  nvarchar(max) ''INVALID'',
         VPID  nvarchar(max) ''VPID'',
         NM  nvarchar(max) ''NM'',
         ABBREVNM  nvarchar(max) ''ABBREVNM'',
         [DESC]  nvarchar(max) ''DESC'',
         NMDT  nvarchar(max) ''NMDT'',
         NM_PREV  nvarchar(max) ''NM_PREV'',
         SUPPCD  nvarchar(max) ''SUPPCD'',
         LIC_AUTHCD  nvarchar(max) ''LIC_AUTHCD'',
         LIC_AUTH_PREVCD  nvarchar(max) ''LIC_AUTH_PREVCD'',
         LIC_AUTHCHANGECD  nvarchar(max) ''LIC_AUTHCHANGECD'',
         LIC_AUTHCHANGEDT  nvarchar(max) ''LIC_AUTHCHANGEDT'',
         COMBPRODCD  nvarchar(max) ''COMBPRODCD'',
         FLAVOURCD  nvarchar(max) ''FLAVOURCD'',
         EMA  nvarchar(max) ''EMA'',
         PARALLEL_IMPORT  nvarchar(max) ''PARALLEL_IMPORT'',
         AVAIL_RESTRICTCD  nvarchar(max) ''AVAIL_RESTRICTCD'',
         AMPS_Id  nvarchar(max) ''AMPS_Id''
        
        
        )
        EXEC sp_xml_removedocument @hDoc
        
	   UPDATE Dmd_Amp_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
	 END
	 
	 -- Insert statements for f_ampp2
	 IF(@TableName=''f_ampp2'')

	 BEGIN
	 PRINT ''''
         EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
         
         INSERT INTO Dmd_Ampp_Changeset (APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id)
         
         SELECT APPID,INVALID,NM,ABBREVNM,VPPID,APID,COMBPACKCD,LEGAL_CATCD,SUBP,DISCCD,DISCDT,AMPPS_Id
         
         FROM OPENXML(@hDoc, ''/ACTUAL_MEDICINAL_PROD_PACKS/AMPPS/AMPP'')
         WITH 
         (
         APPID nvarchar(max)  ''APPID'',
         INVALID nvarchar(max) ''INVALID'',
         NM nvarchar(max) ''NM'',
         ABBREVNM nvarchar(max) ''ABBREVNM'',
         VPPID nvarchar(max) ''VPPID'',
         APID nvarchar(max) ''APID'',
         COMBPACKCD nvarchar(max) ''COMBPACKCD'',
         LEGAL_CATCD nvarchar(max) ''LEGAL_CATCD'',
         SUBP nvarchar(max) ''SUBP'',
         DISCCD nvarchar(max) ''DISCCD'',
         DISCDT nvarchar(max) ''DISCDT'',
         AMPPS_Id nvarchar(max) ''AMPPS_Id''
         
         
         )
         EXEC sp_xml_removedocument @hDoc

		 UPDATE Dmd_Ampp_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL

	   END


	  -- Insert statements for f_lookup2

     IF(@TableName=''f_lookup2'')

	 BEGIN	
	 ----############# FORM ######################  -
           EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData
          
           INSERT INTO Dmd_Form_Changeset (CD,CDDT,CDPREV,[DESC])
           SELECT CD, CDDT, CDPREV,[DESC]
           FROM OPENXML(@hDoc, ''/LOOKUP/FORM/INFO'')
           WITH 
           (
           CD nvarchar(max) ''CD'',
           CDDT nvarchar(max) ''CDDT'',
           CDPREV nvarchar(max) ''CDPREV'',
           [DESC] nvarchar(max) ''DESC''    
           )
           EXEC sp_xml_removedocument @hDoc
      ----############# ROUTE ######################  -
	   
	       EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData

	       INSERT INTO Dmd_ROUTE_Changeset (CD,[DESC])
           SELECT CD,[DESC]
           FROM OPENXML(@hDoc, ''/LOOKUP/ROUTE/INFO'')
           WITH 
           (
           CD nvarchar(max) ''CD'',          
           [DESC] nvarchar(max) ''DESC''          
          
           )
           EXEC sp_xml_removedocument @hDoc
	     ----############# SUPPLIER ######################  -
		  
		   EXEC sp_xml_preparedocument @hDoc OUTPUT, @xmlData

	       INSERT INTO Dmd_SUPPLIER_Changeset (CD,[DESC])
           SELECT CD,[DESC]
           FROM OPENXML(@hDoc, ''/LOOKUP/SUPPLIER/INFO'')
           WITH 
           (
           CD nvarchar(max) ''CD'',          
           [DESC] nvarchar(max) ''DESC''          
          
           )
           EXEC sp_xml_removedocument @hDoc
		      
		   UPDATE Dmd_Form_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
		   UPDATE Dmd_ROUTE_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
		   UPDATE Dmd_SUPPLIER_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL
	   END

	   IF(@TableName=''f_gtin2'')

	   BEGIN	

	    INSERT INTO Dmd_Gtin_Changeset (AMPPID,GTINDATAGTIN,GTINDATASTARTDT)
		SELECT
		GTIN.detail.value(''(AMPPID/text())[1]'',''VARCHAR(100)'') AS AMPPID, --TAG
		GTIN.detail.value(''(GTINDATA/GTIN/text())[1]'',''VARCHAR(100)'') AS GTIN, --TAG
		GTIN.detail.value(''(GTINDATA/STARTDT/text())[1]'',''VARCHAR(100)'') AS GTIN
		FROM
		@xmlData.nodes(''GTIN_DETAILS/AMPPS/AMPP'')AS GTIN(detail)

		UPDATE Dmd_Gtin_Changeset SET ChangesetDate=GETDATE(),ChangsetId= @DmdChangeSetDetailID WHERE ChangsetId IS NULL

	 END
END







' 
END
GO
/****** Object:  StoredProcedure [dbo].[SP_DmdProcessXMLFileToChangeset]    Script Date: 07/14/2018 21:15:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DmdProcessXMLFileToChangeset]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCedure [dbo].[SP_DmdProcessXMLFileToChangeset]
AS
BEGIN

DECLARE @TotalRowCount int,
@CurrentRow int =1 ,
@FilePath varchar(max),
@sql nvarchar(max),
@outputxml xml,
@TableName varchar(50),
@UploadedFileID int,
@ChangesetID varchar(50),
@DmdChangeSetDetailID int


CREATE Table #TempUploadedFiles
(CurrentID int Identity(1,1),
UploadedFileID int ,
FilePath varchar(max),
TableName varchar(50),
ChangesetID varchar(50),
[WeekNo] [varchar](50) NULL,
[YearNo] [varchar](50) NULL,
[ChangesetFileSize] decimal(9,2) NULL
)


SET @TotalRowCount =(Select Count(*) from UploadedFiles (NOLOCK) where Isprocessed=0 and Isdeleted=0 and 
cast(createdon as date) =cast(getdate() as date))


insert into #TempUploadedFiles
select UploadedFileID,FilePath,TableName,ChangesetID,WeekNo,YearNo,ChangesetFileSize from UploadedFiles (NOLOCK) where Isprocessed=0 and Isdeleted=0 and 
cast(createdon as date) =cast(getdate() as date)

select distinct @ChangesetID =ChangesetID  from #TempUploadedFiles

IF(ISNULL(@ChangesetID,'''') <> '''')
BEGIN


	insert into [Dmd_ChangeSetDetails] (Description,Isprocessed,WeekNo,YearNo,ChangesetFileSize)
	select distinct @ChangesetID,1,WeekNo,YearNo,ChangesetFileSize  from #TempUploadedFiles
	
	SET @DmdChangeSetDetailID = SCOPE_IDENTITY() 	

END



While(@CurrentRow <= @TotalRowCount)

BEGIN

SELECT @FilePath  = FilePath,
	   @TableName =TableName,
	   @UploadedFileID=UploadedFileID FROM #TempUploadedFiles WHERE CurrentID = @CurrentRow 	   


 select @sql = ''select @xmlOut = x.BulkColumn from openrowset(
     bulk '''''' + @FilePath + '''''', single_blob) x'';
 
 
IF (select dbo.fc_FileExists(@FilePath) ) = 1
BEGIN
exec sp_executesql @sql, N''@xmlOut xml out'', @xmlOut = @outputxml out;

update UploadedFiles set XmlData=null,Isprocessed=1 WHERE UploadedFileID = @UploadedFileID 

exec SP_DmdProcessXMLToStaging @outputxml,@TableName,@DmdChangeSetDetailID

--DELETE FROM #TempUploadedFiles WHERE CurrentID = @CurrentRow 	
END

SET  @CurrentRow =@CurrentRow +1  

  
END

IF(ISNULL(@DmdChangeSetDetailID,0) <> 0)
BEGIN


DROP Table #TempUploadedFiles
exec [SP_DmdProcessStagingToHistory] @DmdChangeSetDetailID
truncate table UploadedFiles
END

END

' 
END
GO
/****** Object:  Table [task].[ReminderInvitationMember]    Script Date: 07/14/2018 21:15:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]') AND type in (N'U'))
BEGIN
CREATE TABLE [task].[ReminderInvitationMember](
	[ReminderInvitationMemberId] [int] IDENTITY(1,1) NOT NULL,
	[ArchivedDate] [datetime2](7) NULL,
	[ArchivedUser] [nvarchar](max) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedUser] [nvarchar](max) NULL,
	[HasRead] [bit] NULL,
	[IsArchived] [bit] NOT NULL,
	[ReadDate] [datetime2](7) NULL,
	[RecipientUserName] [nvarchar](max) NULL,
	[ReminderInvitationId] [int] NOT NULL,
	[ReminderProgressId] [int] NOT NULL,
	[UpdatedDate] [datetime2](7) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
 CONSTRAINT [PK_ReminderInvitationMember] PRIMARY KEY CLUSTERED 
(
	[ReminderInvitationMemberId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Permission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsShowMenu] [bit] NULL,
	[Key] [nvarchar](200) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ParentPermissionId] [int] NOT NULL,
	[Sort] [int] NULL,
	[Url] [nvarchar](200) NOT NULL,
	[UserModuleId] [int] NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TrustAddresses]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TrustAddresses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TrustAddresses](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[TrustId] [int] NOT NULL,
 CONSTRAINT [PK_TrustAddresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[DosageForm]    Script Date: 07/14/2018 21:15:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DosageForm]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DosageForm](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DmdFormId] [bigint] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[UpdatedUser] [nvarchar](max) NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[DosageFormType_Id] [int] NULL,
	[Status_Id] [int] NULL,
 CONSTRAINT [PK_dbo.DosageForm] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PermissionGroup]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PermissionGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PermissionGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_PermissionGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RolePermission]    Script Date: 07/14/2018 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RolePermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RolePermission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionGroupId] [int] NULL,
	[PermissionId] [int] NULL,
	[RoleId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RolePermission] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_Addresses_AddressType_AddressTypeId]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Addresses_AddressType_AddressTypeId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Addresses]'))
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_AddressType_AddressTypeId] FOREIGN KEY([AddressTypeId])
REFERENCES [dbo].[AddressType] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Addresses_AddressType_AddressTypeId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Addresses]'))
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_AddressType_AddressTypeId]
GO
/****** Object:  ForeignKey [FK_dbo.AdministrationMethod_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AdministrationMethod_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]'))
ALTER TABLE [dbo].[AdministrationMethod]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.AdministrationMethod_dbo.Status_Status_Id] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AdministrationMethod_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdministrationMethod]'))
ALTER TABLE [dbo].[AdministrationMethod] NOCHECK CONSTRAINT [FK_dbo.AdministrationMethod_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_AspNetRoleClaims_AspNetRoles_RoleId]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetRoleClaims_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetRoleClaims]'))
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_AspNetUserLogins_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_IdentityUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserLogins_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_AspNetUserRoles_AspNetRoles_RoleId]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
/****** Object:  ForeignKey [FK_AspNetUserRoles_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_IdentityUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_AspNetUserTokens_IdentityUser_UserId]    Script Date: 07/14/2018 21:15:46 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_IdentityUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[IdentityUser] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AspNetUserTokens_IdentityUser_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserTokens]'))
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_IdentityUser_UserId]
GO
/****** Object:  ForeignKey [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id] FOREIGN KEY([DosageFormType_Id])
REFERENCES [dbo].[DosageFormType] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] NOCHECK CONSTRAINT [FK_dbo.DosageForm_dbo.DosageFormType_DosageFormType_Id]
GO
/****** Object:  ForeignKey [FK_dbo.DosageForm_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.DosageForm_dbo.Status_Status_Id] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageForm_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageForm]'))
ALTER TABLE [dbo].[DosageForm] NOCHECK CONSTRAINT [FK_dbo.DosageForm_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_dbo.DosageFormType_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageFormType_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageFormType]'))
ALTER TABLE [dbo].[DosageFormType]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.DosageFormType_dbo.Status_Status_Id] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.DosageFormType_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[DosageFormType]'))
ALTER TABLE [dbo].[DosageFormType] NOCHECK CONSTRAINT [FK_dbo.DosageFormType_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_dbo.Manufacturer_dbo.Status_Status_Id]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Manufacturer_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Manufacturer]'))
ALTER TABLE [dbo].[Manufacturer]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.Manufacturer_dbo.Status_Status_Id] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Status] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.Manufacturer_dbo.Status_Status_Id]') AND parent_object_id = OBJECT_ID(N'[dbo].[Manufacturer]'))
ALTER TABLE [dbo].[Manufacturer] NOCHECK CONSTRAINT [FK_dbo.Manufacturer_dbo.Status_Status_Id]
GO
/****** Object:  ForeignKey [FK_Permission_Module_ModuleId]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_Module_ModuleId] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_Module_ModuleId]
GO
/****** Object:  ForeignKey [FK_Permission_UserModule_UserModuleId]    Script Date: 07/14/2018 21:15:50 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_UserModule_UserModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission]  WITH CHECK ADD  CONSTRAINT [FK_Permission_UserModule_UserModuleId] FOREIGN KEY([UserModuleId])
REFERENCES [dbo].[UserModule] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permission_UserModule_UserModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[Permission]'))
ALTER TABLE [dbo].[Permission] CHECK CONSTRAINT [FK_Permission_UserModule_UserModuleId]
GO
/****** Object:  ForeignKey [FK_PermissionGroup_Permission_PermissionId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionGroup_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionGroup]'))
ALTER TABLE [dbo].[PermissionGroup]  WITH CHECK ADD  CONSTRAINT [FK_PermissionGroup_Permission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PermissionGroup_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[PermissionGroup]'))
ALTER TABLE [dbo].[PermissionGroup] CHECK CONSTRAINT [FK_PermissionGroup_Permission_PermissionId]
GO
/****** Object:  ForeignKey [FK_RolePermission_Permission_PermissionId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_Permission_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permission] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_Permission_PermissionId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_Permission_PermissionId]
GO
/****** Object:  ForeignKey [FK_RolePermission_PermissionGroup_PermissionGroupId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_PermissionGroup_PermissionGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission]  WITH CHECK ADD  CONSTRAINT [FK_RolePermission_PermissionGroup_PermissionGroupId] FOREIGN KEY([PermissionGroupId])
REFERENCES [dbo].[PermissionGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_RolePermission_PermissionGroup_PermissionGroupId]') AND parent_object_id = OBJECT_ID(N'[dbo].[RolePermission]'))
ALTER TABLE [dbo].[RolePermission] CHECK CONSTRAINT [FK_RolePermission_PermissionGroup_PermissionGroupId]
GO
/****** Object:  ForeignKey [FK_TrustAddresses_Addresses_AddressId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Addresses_AddressId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses]  WITH CHECK ADD  CONSTRAINT [FK_TrustAddresses_Addresses_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Addresses] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Addresses_AddressId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] CHECK CONSTRAINT [FK_TrustAddresses_Addresses_AddressId]
GO
/****** Object:  ForeignKey [FK_TrustAddresses_Trusts_TrustId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses]  WITH CHECK ADD  CONSTRAINT [FK_TrustAddresses_Trusts_TrustId] FOREIGN KEY([TrustId])
REFERENCES [trusts].[Trusts] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustAddresses_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustAddresses]'))
ALTER TABLE [dbo].[TrustAddresses] CHECK CONSTRAINT [FK_TrustAddresses_Trusts_TrustId]
GO
/****** Object:  ForeignKey [FK_TrustContacts_Contacts_ContactId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Contacts_ContactId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts]  WITH CHECK ADD  CONSTRAINT [FK_TrustContacts_Contacts_ContactId] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contacts] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Contacts_ContactId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] CHECK CONSTRAINT [FK_TrustContacts_Contacts_ContactId]
GO
/****** Object:  ForeignKey [FK_TrustContacts_Trusts_TrustId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts]  WITH CHECK ADD  CONSTRAINT [FK_TrustContacts_Trusts_TrustId] FOREIGN KEY([TrustId])
REFERENCES [trusts].[Trusts] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TrustContacts_Trusts_TrustId]') AND parent_object_id = OBJECT_ID(N'[dbo].[TrustContacts]'))
ALTER TABLE [dbo].[TrustContacts] CHECK CONSTRAINT [FK_TrustContacts_Trusts_TrustId]
GO
/****** Object:  ForeignKey [FK_UserModule_Module_ModuleId]    Script Date: 07/14/2018 21:15:51 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserModule_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserModule]'))
ALTER TABLE [dbo].[UserModule]  WITH CHECK ADD  CONSTRAINT [FK_UserModule_Module_ModuleId] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserModule_Module_ModuleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserModule]'))
ALTER TABLE [dbo].[UserModule] CHECK CONSTRAINT [FK_UserModule_Module_ModuleId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitation_Reminder_ReminderId]    Script Date: 07/14/2018 21:15:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitation_Reminder_ReminderId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitation]'))
ALTER TABLE [task].[ReminderInvitation]  WITH CHECK ADD  CONSTRAINT [FK_ReminderInvitation_Reminder_ReminderId] FOREIGN KEY([ReminderId])
REFERENCES [task].[Reminder] ([ReminderId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitation_Reminder_ReminderId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitation]'))
ALTER TABLE [task].[ReminderInvitation] CHECK CONSTRAINT [FK_ReminderInvitation_Reminder_ReminderId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]    Script Date: 07/14/2018 21:15:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember]  WITH CHECK ADD  CONSTRAINT [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId] FOREIGN KEY([ReminderInvitationId])
REFERENCES [task].[ReminderInvitation] ([ReminderInvitationId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] CHECK CONSTRAINT [FK_ReminderInvitationMember_ReminderInvitation_ReminderInvitationId]
GO
/****** Object:  ForeignKey [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]    Script Date: 07/14/2018 21:15:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember]  WITH CHECK ADD  CONSTRAINT [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId] FOREIGN KEY([ReminderProgressId])
REFERENCES [task].[ReminderProgress] ([ReminderProgressId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderInvitationMember]'))
ALTER TABLE [task].[ReminderInvitationMember] CHECK CONSTRAINT [FK_ReminderInvitationMember_ReminderProgress_ReminderProgressId]
GO
/****** Object:  ForeignKey [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]    Script Date: 07/14/2018 21:15:52 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderProgress]'))
ALTER TABLE [task].[ReminderProgress]  WITH CHECK ADD  CONSTRAINT [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId] FOREIGN KEY([ReminderProgressStatusId])
REFERENCES [task].[ReminderProgressStatus] ([ReminderProgressStatusId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[task].[FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]') AND parent_object_id = OBJECT_ID(N'[task].[ReminderProgress]'))
ALTER TABLE [task].[ReminderProgress] CHECK CONSTRAINT [FK_ReminderProgress_ReminderProgressStatus_ReminderProgressStatusId]
GO
