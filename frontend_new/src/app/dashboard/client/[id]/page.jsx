import Navbar from "@/components/dashboard/Navbar";
import styles from "@/styles/Dashboard/Main.module.css"


function ClientDashboard() {
    return (
        <>
            <div className={styles.mainPageContainer}>
                <Navbar />

            </div>
        </>
    );
}

export default ClientDashboard;