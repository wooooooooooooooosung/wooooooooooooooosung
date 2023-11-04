import win32serviceutil
import servicemanager
import win32service
import win32event
import time

class LogTestService(win32serviceutil.ServiceFramework):
    _svc_name_ = "LogTestService2"
    _svc_display_name_ = "Log Test Service2"
    _svc_description_ = "Advanced Programming2 Engaged Learning"

    #log_file = open('C:/Users/ssu-sw/Desktop/ServiceLog.txt', 'w')
    
    def __init__(self, args):
        log_file = open('C:/Users/ssu-sw/Desktop/ServiceLog.txt', 'a')
        log_file.write("[ " + time.ctime() + " ] 설치\n")
        log_file.flush()
        win32serviceutil.ServiceFramework.__init__(self, args)
        self.hWaitStop = win32event.CreateEvent(None, 0, 0, None)

    def SvcStop(self):
        log_file = open('C:/Users/ssu-sw/Desktop/ServiceLog.txt', 'a')
        log_file.write("[ " + time.ctime() + " ] 중지\n")
        log_file.flush()
        self.ReportServiceStatus(win32service.SERVICE_STOP_PENDING)
        win32event.SetEvent(self.hWaitStop)

    def SvcDoRun(self):
        log_file = open('C:/Users/ssu-sw/Desktop/ServiceLog.txt', 'a')
        log_file.write("[ " + time.ctime() + " ] 구동\n")
        log_file.flush()
        servicemanager.LogMsg(servicemanager.EVENTLOG_INFORMATION_TYPE,
                             servicemanager.PYS_SERVICE_STARTED,
                             (self._svc_name_, ''))
        self.main()

    def main(self):
        
        log_file = open('C:/Users/ssu-sw/Desktop/ServiceLog.txt', 'a')
        while True:
            # 1초마다 현재 시간을 로그 파일에 기록
            log_file.write("[ " + time.ctime() + " ] 구동중\n")
            log_file.flush()
            time.sleep(1)

            # 서비스 중지 이벤트를 확인
            if win32event.WaitForSingleObject(self.hWaitStop, 0) == win32event.WAIT_OBJECT_0:
                break

        log_file.close()  # 로그 파일 닫기

# 시작점
if __name__ == '__main__':
    win32serviceutil.HandleCommandLine(LogTestService)
