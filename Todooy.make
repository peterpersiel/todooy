

# Warning: This is an automatically generated file, do not edit!

if ENABLE_DEBUG_IPHONESIMULATOR
ASSEMBLY_COMPILER_COMMAND = smcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/iPhoneSimulator/Debug/Todooy.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/iPhoneSimulator/Debug

TODOOY_EXE_MDB_SOURCE=bin/iPhoneSimulator/Debug/Todooy.exe.mdb
TODOOY_EXE_MDB=$(BUILD_DIR)/Todooy.exe.mdb
TASKY_EXE_MDB=

endif

if ENABLE_RELEASE_IPHONESIMULATOR
ASSEMBLY_COMPILER_COMMAND = smcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/iPhoneSimulator/Release/Todooy.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/iPhoneSimulator/Release

TODOOY_EXE_MDB=
TASKY_EXE_MDB=

endif

if ENABLE_DEBUG_IPHONE
ASSEMBLY_COMPILER_COMMAND = smcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize- -debug "-define:DEBUG;"
ASSEMBLY = bin/iPhone/Debug/Tasky.exe
ASSEMBLY_MDB = $(ASSEMBLY).mdb
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/iPhone/Debug

TODOOY_EXE_MDB=
TASKY_EXE_MDB_SOURCE=bin/iPhone/Debug/Tasky.exe.mdb
TASKY_EXE_MDB=$(BUILD_DIR)/Tasky.exe.mdb

endif

if ENABLE_RELEASE_IPHONE
ASSEMBLY_COMPILER_COMMAND = smcs
ASSEMBLY_COMPILER_FLAGS =  -noconfig -codepage:utf8 -warn:4 -optimize-
ASSEMBLY = bin/iPhone/Release/Tasky.exe
ASSEMBLY_MDB = 
COMPILE_TARGET = exe
PROJECT_REFERENCES = 
BUILD_DIR = bin/iPhone/Release

TODOOY_EXE_MDB=
TASKY_EXE_MDB=

endif

AL=al
SATELLITE_ASSEMBLY_NAME=$(notdir $(basename $(ASSEMBLY))).resources.dll

PROGRAMFILES = \
	$(TODOOY_EXE_MDB) \
	$(TASKY_EXE_MDB)  

BINARIES = \
	$(TODOOY)  


RESGEN=resgen2
	
all: $(ASSEMBLY) $(PROGRAMFILES) $(BINARIES) 

FILES = \
	Main.cs \
	AppDelegate.cs \
	Core/Task.cs \
	Core/TaskManager.cs \
	Core/Category.cs \
	Core/DatabaseADO.cs \
	Core/RepositoryADO.cs \
	Core/CategoryManager.cs \
	Screens/TasksScreen.cs \
	Screens/CatergoriesScreen.cs \
	ApplicationLayer/TaskSource.cs \
	ApplicationLayer/CategorySource.cs \
	Extensions/Epoch.cs 

DATA_FILES = 

RESOURCES = 

EXTRAS = \
	Info.plist \
	.gitignore \
	Screens \
	ApplicationLayer \
	Resources \
	Core \
	Extensions \
	todooy.in 

REFERENCES =  \
	System \
	System.Core \
	monotouch \
	MonoTouch.Dialog-1 \
	Mono.Data.Sqlite \
	System.Data

DLL_REFERENCES = 

CLEANFILES = $(PROGRAMFILES) $(BINARIES) 

include $(top_srcdir)/Makefile.include

TODOOY = $(BUILD_DIR)/todooy

$(eval $(call emit-deploy-wrapper,TODOOY,todooy,x))


$(eval $(call emit_resgen_targets))
$(build_xamlg_list): %.xaml.g.cs: %.xaml
	xamlg '$<'

$(ASSEMBLY_MDB): $(ASSEMBLY)

$(ASSEMBLY): $(build_sources) $(build_resources) $(build_datafiles) $(DLL_REFERENCES) $(PROJECT_REFERENCES) $(build_xamlg_list) $(build_satellite_assembly_list)
	mkdir -p $(shell dirname $(ASSEMBLY))
	$(ASSEMBLY_COMPILER_COMMAND) $(ASSEMBLY_COMPILER_FLAGS) -out:$(ASSEMBLY) -target:$(COMPILE_TARGET) $(build_sources_embed) $(build_resources_embed) $(build_references_ref)
