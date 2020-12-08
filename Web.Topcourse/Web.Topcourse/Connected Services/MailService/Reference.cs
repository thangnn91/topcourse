﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.Topcourse.MailService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EmailModel", Namespace="http://schemas.datacontract.org/2004/07/MailService.Model")]
    [System.SerializableAttribute()]
    public partial class EmailModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ContentField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsResendField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int LangIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int MailIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ParamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ServiceIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SignField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TitleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ToMailField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Content {
            get {
                return this.ContentField;
            }
            set {
                if ((object.ReferenceEquals(this.ContentField, value) != true)) {
                    this.ContentField = value;
                    this.RaisePropertyChanged("Content");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsResend {
            get {
                return this.IsResendField;
            }
            set {
                if ((this.IsResendField.Equals(value) != true)) {
                    this.IsResendField = value;
                    this.RaisePropertyChanged("IsResend");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int LangID {
            get {
                return this.LangIDField;
            }
            set {
                if ((this.LangIDField.Equals(value) != true)) {
                    this.LangIDField = value;
                    this.RaisePropertyChanged("LangID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int MailID {
            get {
                return this.MailIDField;
            }
            set {
                if ((this.MailIDField.Equals(value) != true)) {
                    this.MailIDField = value;
                    this.RaisePropertyChanged("MailID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Param {
            get {
                return this.ParamField;
            }
            set {
                if ((object.ReferenceEquals(this.ParamField, value) != true)) {
                    this.ParamField = value;
                    this.RaisePropertyChanged("Param");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ServiceID {
            get {
                return this.ServiceIDField;
            }
            set {
                if ((this.ServiceIDField.Equals(value) != true)) {
                    this.ServiceIDField = value;
                    this.RaisePropertyChanged("ServiceID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sign {
            get {
                return this.SignField;
            }
            set {
                if ((object.ReferenceEquals(this.SignField, value) != true)) {
                    this.SignField = value;
                    this.RaisePropertyChanged("Sign");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Title {
            get {
                return this.TitleField;
            }
            set {
                if ((object.ReferenceEquals(this.TitleField, value) != true)) {
                    this.TitleField = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ToMail {
            get {
                return this.ToMailField;
            }
            set {
                if ((object.ReferenceEquals(this.ToMailField, value) != true)) {
                    this.ToMailField = value;
                    this.RaisePropertyChanged("ToMail");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ResponseData", Namespace="http://schemas.datacontract.org/2004/07/MailService.Model")]
    [System.SerializableAttribute()]
    public partial class ResponseData : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResponseCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ResponseCode {
            get {
                return this.ResponseCodeField;
            }
            set {
                if ((this.ResponseCodeField.Equals(value) != true)) {
                    this.ResponseCodeField = value;
                    this.RaisePropertyChanged("ResponseCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MailService.ISendMailService")]
    public interface ISendMailService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISendMailService/SendEmailManual", ReplyAction="http://tempuri.org/ISendMailService/SendEmailManualResponse")]
        Web.Topcourse.MailService.ResponseData SendEmailManual(Web.Topcourse.MailService.EmailModel requestSendMail);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISendMailService/SendEmailManual", ReplyAction="http://tempuri.org/ISendMailService/SendEmailManualResponse")]
        System.Threading.Tasks.Task<Web.Topcourse.MailService.ResponseData> SendEmailManualAsync(Web.Topcourse.MailService.EmailModel requestSendMail);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISendMailServiceChannel : Web.Topcourse.MailService.ISendMailService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SendMailServiceClient : System.ServiceModel.ClientBase<Web.Topcourse.MailService.ISendMailService>, Web.Topcourse.MailService.ISendMailService {
        
        public SendMailServiceClient() {
        }
        
        public SendMailServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SendMailServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendMailServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SendMailServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Web.Topcourse.MailService.ResponseData SendEmailManual(Web.Topcourse.MailService.EmailModel requestSendMail) {
            return base.Channel.SendEmailManual(requestSendMail);
        }
        
        public System.Threading.Tasks.Task<Web.Topcourse.MailService.ResponseData> SendEmailManualAsync(Web.Topcourse.MailService.EmailModel requestSendMail) {
            return base.Channel.SendEmailManualAsync(requestSendMail);
        }
    }
}
