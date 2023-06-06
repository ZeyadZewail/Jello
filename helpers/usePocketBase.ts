"use client";

import { useEffect, useState } from "react";
import PocketBase, { BaseAuthStore } from "pocketbase";
import { useRouter, usePathname } from "next/navigation";

const usePocketBase = () => {
	const pathname = usePathname();
	const { push } = useRouter();
	const pb = new PocketBase("http://127.0.0.1:8090");
	const [authStore, setAuthStore] = useState<BaseAuthStore>();

	useEffect(() => {
		setAuthStore(pb.authStore);

		if (pathname === "/login") {
			return;
		}
		if (!pb?.authStore.isValid) {
			push("/login");
		}
	}, []);

	return { pb, authStore };
};

export default usePocketBase;
