#pragma once
#pragma pack(1)
typedef struct tagIDCardData{
	char Name[32]; //����       
	char Sex[4];   //�Ա�
	char Nation[6]; //����
	char Born[18]; //��������
	char Address[72]; //סַ
	char IDCardNo[38]; //���֤��
	char GrantDept[32]; //��֤����
	char UserLifeBegin[18]; // ��Ч��ʼ����
	char UserLifeEnd[18];  // ��Ч��ֹ����
	char reserved[38]; // ����
	char PhotoFileName[255]; // ��Ƭ·��
}IDCardData;

extern "C"{
int _stdcall Syn_GetCOMBaud(int iComID,unsigned int *puiBaud);
int _stdcall Syn_SetCOMBaud(int iComID,unsigned int  uiCurrBaud,unsigned int  uiSetBaud);
int _stdcall Syn_OpenPort(int iPortID);
int _stdcall Syn_ClosePort(int iPortID);

/**********************************************************
 ********************** SAM��API **************************
 **********************************************************/
int _stdcall Syn_GetSAMStatus(int iPortID,int iIfOpen);
int _stdcall Syn_ResetSAM(int iPortID,int iIfOpen);
int _stdcall Syn_GetSAMID(int iPortID,unsigned char *pucSAMID,int iIfOpen);
int _stdcall Syn_GetSAMIDToStr(int iPortID,char *pcSAMID,int iIfOpen);

/**********************************************************
 ******************* ���֤����API ************************
 **********************************************************/
int _stdcall Syn_StartFindIDCard(int iPortID,unsigned char *pucManaInfo,int iIfOpen);
int _stdcall Syn_SelectIDCard(int iPortID,unsigned char *pucManaMsg,int iIfOpen);
int _stdcall Syn_ReadMsg(int iPortID,int iIfOpen,IDCardData *pIDCardData);
int _stdcall Syn_ReadCard(int Rmode);

/**********************************************************
 ******************* ������API ************************
 **********************************************************/
int  _stdcall Syn_GetBmp(char * Wlt_File,int intf);
int  _stdcall Syn_SendSound(int iCmdNo);
void _stdcall Syn_DelPhotoFile();

CString _stdcall  Syn_Name();
CString _stdcall  Syn_Sex();
CString _stdcall  Syn_Nation();
CString _stdcall  Syn_Born();
CString _stdcall  Syn_Address();
CString _stdcall  Syn_IDCardNo();
CString _stdcall  Syn_GrantDept();
CString _stdcall  Syn_UserLifeBegin();
CString _stdcall  Syn_UserLifeEnd();
CString _stdcall  Syn_Reserved();
CString _stdcall  Syn_PhotoFileName();
CString _stdcall  Syn_GetState();
}