pipeline {
    agent any
    environment {
        REPO = "distanceed/distanceedregistration"
        PRIVATE_REPO = "${PRIVATE_DOCKER_REGISTRY}/${REPO}"
        PRIVATE_REPO_EMR = "${PRIVATE_DOCKER_REGISTRY}/${REPO}-emailrunner"
        PRIVATE_REPO_ADMIN = "${PRIVATE_DOCKER_REGISTRY}/${REPO}-admin"
        TAG = "${BUILD_TIMESTAMP}"
    }
    stages {
        stage('Git clone') {
            steps {
                git branch: 'master',
                    url: 'https://github.com/LivingSkySchoolDivision/DistanceEdRegistrationForm.git'
            }
        }
        stage('Test') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/core/sdk:3.1'
                    args '-v ${PWD}:/app'
                }
            }
            steps {
                git branch: 'master',
                    url: 'https://github.com/LivingSkySchoolDivision/DistanceEdRegistrationForm.git'

                dir("LSSDDistanceEdReg"){
                    sh 'dotnet build'
                    sh 'dotnet test'
                }
            }
        }
        stage('Docker build') {
            steps {
                dir("LSSDDistanceEdReg"){
                    sh "docker build --no-cache -f Dockerfile-WebFrontEnd -t ${PRIVATE_REPO}:latest -t ${PRIVATE_REPO}:${TAG} ."
                    sh "docker build --no-cache -f Dockerfile-EmailRunner -t ${PRIVATE_REPO_EMR}:latest -t ${PRIVATE_REPO_EMR}:${TAG} ."
                    sh "docker build --no-cache -f Dockerfile-AdminFrontEnd -t ${PRIVATE_REPO_ADMIN}:latest -t ${PRIVATE_REPO_ADMIN}:${TAG} ."
                }
            }
        }
        stage('Docker push') {
            steps {
                sh "docker push ${PRIVATE_REPO}:${TAG}"
                sh "docker push ${PRIVATE_REPO}:latest"
                sh "docker push ${PRIVATE_REPO_EMR}:${TAG}"
                sh "docker push ${PRIVATE_REPO_EMR}:latest"
                sh "docker push ${PRIVATE_REPO_ADMIN}:${TAG}"
                sh "docker push ${PRIVATE_REPO_ADMIN}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}
